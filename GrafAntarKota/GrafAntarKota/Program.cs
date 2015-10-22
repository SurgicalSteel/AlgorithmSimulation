using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//dedicated to Melinda Orientanti
//get a life bray, kelamaan di depan IDE tuh ngga sehat
//tidur woy...

namespace GrafAntarKota
{
    class Program
    {
        static void isidata(FakeGraph fg) 
        {
            fg.insertParentNode("Purworejo");
            fg.insertParentNode("Magelang");
            fg.insertParentNode("Temanggung");
            fg.insertParentNode("Wonosobo");
            fg.insertParentNode("Kebumen");
            fg.insertParentNode("Kulon Progo");
            fg.insertParentNode("Banyumas");
            fg.insertRelation("Purworejo", "Kebumen", 40);
            fg.insertRelation("Purworejo", "Magelang", 25);
            fg.insertRelation("Purworejo", "Wonosobo", 30);
            fg.insertRelation("Purworejo", "Kulon Progo", 35);
            fg.insertRelation("Wonosobo", "Kebumen", 25);
            fg.insertRelation("Magelang", "Wonosobo", 20);
            fg.insertRelation("Magelang", "Temanggung", 35);
            fg.insertRelation("Wonosobo", "Temanggung", 30);
            fg.insertRelation("Banyumas", "Kebumen", 30);
        }
        static Boolean sudahdiscoverkah(List<ParentNode> discoveredlist, ParentNode nodetarget) 
        {
            Boolean found = false;
            for (int i = 0; i < discoveredlist.Count(); i++) 
            {
                if(discoveredlist.ElementAt(i).Equals(nodetarget))
                {
                    found=true;
                    break;
                }
            }
            return found;
        }
        static void DFS(FakeGraph fg, string kotaasal, string kotadicari) 
        {
            ParentNode v = fg.pn[fg.findindex(kotaasal)];
            List<ParentNode> discovered = new List<ParentNode>();
            Stack<ParentNode> tumpukan = new Stack<ParentNode>();
            tumpukan.Push(v);

            while (!tumpukan.Count.Equals(0))
            {
                v = tumpukan.Pop();
                if (!sudahdiscoverkah(discovered, v))
                {
                    discovered.Add(v);
                    if (v.Equals(fg.pn[fg.findindex(kotadicari)])) 
                    {
                        Console.WriteLine("Kota Ditemukan!");
                        Console.WriteLine("Jalur Penelusuran    :");
                        for (int i = 0; i < discovered.Count(); i++)
                        {
                            Console.WriteLine(discovered.ElementAt(i).nama);
                        }
                        break;
                    }
                    Queue<ParentNode> tetangga = new Queue<ParentNode>();
                    tetangga = fg.tetangganyadarikota(v);
                    for (int i = 0; i < tetangga.Count(); i++)
                    {
                        tumpukan.Push(tetangga.ElementAt(i));
                    }
                }
            }
            
        }
        static void BFS(FakeGraph fg, string kotaasal, string kotadicari)
        {
            ParentNode v = fg.pn[fg.findindex(kotaasal)];
            List<ParentNode> discovered = new List<ParentNode>();
            Queue<ParentNode> antrian = new Queue<ParentNode>();

            antrian.Enqueue(v);
            discovered.Add(v);
            while (!antrian.Count.Equals(0))
            {
                v = antrian.Dequeue();
                Queue<ParentNode> tetanggaku = new Queue<ParentNode>();
                tetanggaku = fg.tetangganyadarikota(v);
                for (int i = 0; i < tetanggaku.Count(); i++)
                {
                    if (!sudahdiscoverkah(discovered, tetanggaku.ElementAt(i)))
                    {
                        antrian.Enqueue(tetanggaku.ElementAt(i));
                        discovered.Add(tetanggaku.ElementAt(i));
                        if (tetanggaku.ElementAt(i).Equals(fg.pn[fg.findindex(kotadicari)]))
                        {
                            Console.WriteLine("Kota Ditemukan!");
                            Console.WriteLine("Jalur Penelusuran    :");
                            for (int y = 0; y < discovered.Count(); y++)
                            {
                                Console.WriteLine(discovered.ElementAt(y).nama);
                            }
                            break;
                        }
                    }
                }
            }
            
        }
        static void DFSTraversal(FakeGraph fg, string kotaasal) 
        {
            ParentNode v = fg.pn[fg.findindex(kotaasal)];
            List<ParentNode> discovered = new List<ParentNode>();
            Stack<ParentNode> tumpukan = new Stack<ParentNode>();
            tumpukan.Push(v);
            
            while (!tumpukan.Count.Equals(0)) 
            {
                v = tumpukan.Pop();
                if (!sudahdiscoverkah(discovered, v)) 
                {
                    discovered.Add(v);
                    Queue<ParentNode> tetangga = new Queue<ParentNode>();
                    tetangga = fg.tetangganyadarikota(v);
                    for (int i = 0; i < tetangga.Count(); i++) 
                    {
                        tumpukan.Push(tetangga.ElementAt(i));
                    }
                }
            }
            Console.WriteLine();
            for (int i = 0; i < discovered.Count(); i++)
            {
                Console.WriteLine(discovered.ElementAt(i).nama);
            }
        }
        static void BFSTraversal(FakeGraph fg, string kotaasal) 
        {
            ParentNode v = fg.pn[fg.findindex(kotaasal)];
            List<ParentNode> discovered = new List<ParentNode>();
            Queue<ParentNode> antrian = new Queue<ParentNode>();
            
            antrian.Enqueue(v);
            discovered.Add(v);
            while (!antrian.Count.Equals(0)) 
            {
                v = antrian.Dequeue();
                Queue<ParentNode> tetanggaku = new Queue<ParentNode>();
                tetanggaku = fg.tetangganyadarikota(v);
                for (int i = 0; i < tetanggaku.Count(); i++) 
                {
                    if(!sudahdiscoverkah(discovered,tetanggaku.ElementAt(i)))
                    {
                        antrian.Enqueue(tetanggaku.ElementAt(i));
                        discovered.Add(tetanggaku.ElementAt(i));
                    }
                }
            }
            Console.WriteLine();
            for (int i = 0; i < discovered.Count(); i++) 
            {
                Console.WriteLine(discovered.ElementAt(i).nama);
            }
            /*
            foreach (ParentNode p in discovered) 
            {
                Console.WriteLine(p.nama);
            }
            */
        }
        static void Main(string[] args)
        {
            FakeGraph fg = new FakeGraph();
            string pil = "n";
            while ((!pil.Equals("x"))||(!pil.Equals("X")))
            {
                Console.WriteLine("1. Insert Kota");
                Console.WriteLine("2. Insert Relasi");
                Console.WriteLine("3. Lihat Tetangga");
                Console.WriteLine("4. Lihat Seluruh Kota");
                Console.WriteLine("5. Hapus Kota");
                Console.WriteLine("6. Hapus Relasi");
                Console.WriteLine("7. Telusuri BFS");
                Console.WriteLine("8. Telusuri DFS");
                Console.WriteLine("9. Isi data");
                Console.WriteLine("10. Search dengan DFS");
                Console.WriteLine("11. Search dengan BFS");
                Console.WriteLine("X. Exit");
                Console.WriteLine("Pilihan anda     :");
                pil = Console.ReadLine();
                if ((pil.Equals("x")) || (pil.Equals("X"))) 
                {
                    Console.WriteLine("Makasih gan!");
                    break;
                }
                switch (pil)
                {
                    case "1":
                        {
                            string namakota;
                            Console.WriteLine("Masukkan Nama Kota   :");
                            namakota = Console.ReadLine();
                            fg.insertParentNode(namakota);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    case "2":
                        {
                            string ks, kd, tempjarak;
                            int jarak;
                            Console.WriteLine("Masukkan Nama Kota 1 :");
                            ks = Console.ReadLine();
                            Console.WriteLine("Masukkan Nama Kota 2 :");
                            kd = Console.ReadLine();
                            Console.WriteLine("Masukkan Jarak antara Kota 1 dan Kota 2  :");
                            tempjarak = Console.ReadLine();
                            int.TryParse(tempjarak, out jarak);
                            fg.insertRelation(ks, kd, jarak);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    case "3":
                        {
                            string namakota;
                            Console.WriteLine("Masukkan Nama Kota   :");
                            namakota = Console.ReadLine();
                            fg.viewTetangga(namakota);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    case "4":
                        {
                            fg.tampilkanSemuaKota();
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    case "5":
                        {
                            string namakota;
                            Console.WriteLine("Masukkan Nama Kota   :");
                            namakota = Console.ReadLine();
                            fg.deleteParentNode(namakota);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    case "6":
                        {
                            string ks,kd;
                            Console.WriteLine("Masukkan Nama Kota 1 :");
                            ks = Console.ReadLine();
                            Console.WriteLine("Masukkan Nama Kota 2 :");
                            kd = Console.ReadLine();
                            fg.deleteRelasi(ks, kd);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    case "7":
                        {
                            string namakota;
                            Console.WriteLine("Masukkan Nama Kota   :");
                            namakota = Console.ReadLine();
                            if (fg.cekada(namakota)) 
                            {
                                BFSTraversal(fg, namakota);
                            }
                            else 
                            {
                                Console.WriteLine("Nama kota {0} tidak ditemukan dalam graph",namakota);
                            }
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    case "8":
                        {
                            string namakota;
                            Console.WriteLine("Masukkan Nama Kota   :");
                            namakota = Console.ReadLine();
                            if (fg.cekada(namakota))
                            {
                                DFSTraversal(fg, namakota);
                            }
                            else
                            {
                                Console.WriteLine("Nama kota {0} tidak ditemukan dalam graph", namakota);
                            }
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    case "9": 
                        {
                            isidata(fg);
                            Console.WriteLine("Done!");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    case "10": 
                        {
                            string ks, kd;
                            Console.WriteLine("Masukkan Nama Kota Asal :");
                            ks = Console.ReadLine();
                            Console.WriteLine("Masukkan Nama Kota Target :");
                            kd = Console.ReadLine();
                            DFS(fg, ks, kd);
                            Console.ReadLine();
                            Console.Clear();
                            break; 
                        }
                    case "11":
                        {
                            string ks, kd;
                            Console.WriteLine("Masukkan Nama Kota Asal :");
                            ks = Console.ReadLine();
                            Console.WriteLine("Masukkan Nama Kota Target :");
                            kd = Console.ReadLine();
                            BFS(fg, ks, kd);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                }

            }
        }
        }
    }

