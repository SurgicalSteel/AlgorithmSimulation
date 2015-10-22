using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafAntarKota
{
    class ParentNode
    {
        public string nama {get;set;}
        public ChildNode[] cn;
        public ParentNode() 
        { 
            cn = new ChildNode[1000];
            for (int i = 0; i < 1000; i++)
            {
                cn[i] = new ChildNode();
                cn[i].namakota = "";
                cn[i].jarak = -1;
            }
        }
        public void addchild(string namakotanya,int jaraknya)
        {
            for (int i = 0; i < 1000; i++) 
            {
                if(cn[i].namakota.Equals(""))
                {
                    cn[i] = new ChildNode();
                    cn[i].namakota = namakotanya;
                    cn[i].jarak = jaraknya;
                    break;
                }
            }
        }
        public void showtetangga() 
        {
            Console.WriteLine();
            for (int i = 0; i < 1000; i++)
            {
                if (!cn[i].namakota.Equals(""))
                {
                    Console.WriteLine("Kota     : {0}", cn[i].namakota);
                    Console.WriteLine("Jarak    : {0}", cn[i].jarak.ToString());
                }
            }
        }
        public Boolean carichild(string namekotenye) 
        {
            Boolean found = false;
            for (int i = 0; i < 1000; i++)
            {
                if (cn[i].namakota.Equals(namekotenye))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }
        public void deletechild(string namakotanya) 
        {
            for (int i = 0; i < 1000; i++)
            {
                if (cn[i].namakota.Equals(namakotanya))
                {
                    cn[i].namakota = "";
                    cn[i].jarak = -1;
                    break;
                }
            }
        }
        public void deleteallchild() 
        {
            for (int i = 0; i < 1000; i++)
            {
                cn[i].namakota = "";
                cn[i].jarak = -1;
            }
        }
    }
}
