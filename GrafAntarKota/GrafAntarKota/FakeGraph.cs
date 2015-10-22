using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafAntarKota
{
    class FakeGraph
    {
        public ParentNode[] pn;
        public FakeGraph() 
        {
            pn = new ParentNode[1000];
            for (int i = 0; i < 1000; i++) 
            {
                pn[i] = new ParentNode();
                pn[i].nama = "";
            }
        }
        public Queue<ParentNode> tetangganyadarikota(ParentNode kota) 
        {
            Queue<ParentNode> tetanggaku = new Queue<ParentNode>();
            int i = 0;
            while (i < 1000) 
            {
                if (!kota.cn[i].namakota.Equals(""))
                {
                    tetanggaku.Enqueue(pn[findindex(kota.cn[i].namakota)]);
                }
                i++;
            }
            return tetanggaku;
        }
        public ParentNode[] daftartetanggakotatertentu(ParentNode kota)
        {
            ParentNode[] tetangganya = new ParentNode[1000];
            int i=0,idxt=0;
            while ( i < 1000) 
            {
                if (!kota.cn[i].namakota.Equals("")) 
                {
                    tetangganya[idxt] = pn[findindex(kota.cn[i].namakota)];
                    idxt++;
                }
                i++;
            }
                return tetangganya;
        }
        public void insertParentNode(string namakota) 
        {
            if (!cekada(namakota))
            {
                for (int i = 0; i < 1000; i++)
                {
                    if (pn[i].nama.Equals(""))
                    {
                        pn[i].nama = namakota;
                        Console.WriteLine("Sukses insert Kota gan!");
                        break;
                    }
                }
            }
            else 
            {
                Console.WriteLine("Kota {0} sudah ada dalam graph!", namakota);
            }
        }
        public int findindex(string namakota) 
        {
            for (int i = 0; i < 1000; i++)
            {
                if (pn[i].nama.Equals(namakota))
                {
                    return i;
                }
            }
            return 0;
        }
        public Boolean cekada(string namakota) 
        {
            Boolean x = false;
            for (int i = 0; i < 1000; i++)
            {
                if (pn[i].nama.Equals(namakota))
                {
                    x = true;
                    break;
                }
            }
            return x;
        }
        public void deleteParentNode(string namakota) 
        {
            if (cekada(namakota))
            {
                pn[findindex(namakota)].deleteallchild();
                pn[findindex(namakota)].nama = "";
                for (int i = 0; i < 1000; i++) 
                {
                    if (!pn[i].nama.Equals(""))
                    {
                        if (pn[i].carichild(namakota)) 
                        {
                            pn[i].deletechild(namakota);
                        }
                    }
                }
                Console.WriteLine("Sukses delete Kota gan!");
            }
            else 
            {
                Console.WriteLine("Kota {0} tidak ditemukan!", namakota);
            }
        }
        public Boolean cekrelasi(string ns, string nd) 
        {
            Boolean isrel = false;
            if ((pn[findindex(ns)].carichild(nd)) && (pn[findindex(nd)].carichild(ns))) 
            {
                isrel = true;
            }
            return isrel;
        }
        public void deleteRelasi(string kotasatu, string kotadua) 
        {
            if (cekada(kotasatu))
            {
                if (cekada(kotadua))
                {
                    if (cekrelasi(kotasatu, kotadua))
                    {
                        pn[findindex(kotasatu)].deletechild(kotadua);
                        pn[findindex(kotadua)].deletechild(kotasatu);
                        Console.WriteLine("Sukses delete relasi gan!");
                    }
                    else
                    {
                        Console.WriteLine("Kota 1 dan Kota 2 tidak berelasi!");
                    }
                }
                else
                {
                    Console.WriteLine("Kota 2 tidak ditemukan!");
                }
            }
            else
            {
                if (cekada(kotadua))
                {
                    Console.WriteLine("Kota 1 tidak ditemukan!");
                }
                else
                {
                    Console.WriteLine("Kota 1 dan Kota 2 tidak ditemukan!");
                }
            }
        }
        public void insertRelation(string kotasatu, string kotadua, int jarak) 
        {
            if (cekada(kotasatu))
            {
                if (cekada(kotadua))
                {
                    if (!cekrelasi(kotasatu, kotadua))
                    {
                        pn[findindex(kotasatu)].addchild(kotadua, jarak);
                        pn[findindex(kotadua)].addchild(kotasatu, jarak);
                        Console.WriteLine("Sukses insert relasi gan!");
                    }
                    else 
                    {
                        Console.WriteLine("Kota 1 dan Kota 2 sudah berelasi!");
                    }
                }
                else 
                {
                    Console.WriteLine("Kota 2 tidak ditemukan!");
                }
              }
            else 
            {
                if (cekada(kotadua))
                {
                    Console.WriteLine("Kota 1 tidak ditemukan!");
                }
                else 
                {
                    Console.WriteLine("Kota 1 dan Kota 2 tidak ditemukan!");
                }
            }
        }
        public void viewTetangga(string namakota) 
        {
            pn[findindex(namakota)].showtetangga();
        }
        public void tampilkanSemuaKota() 
        {
            Console.WriteLine();
            for (int i = 0; i < 1000; i++)
            {
                if (!pn[i].nama.Equals(""))
                {
                    Console.WriteLine(pn[i].nama);
                }
            }
        }
    }
    
}
