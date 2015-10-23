#include <iostream>
#include <conio.h>
#include <stdlib.h>
#include <algorithm>
#include <stdio.h>
#include <windows.h>

//Created by Yuwono Bangun Nagoro
using namespace std;
class board
{
    public:
    int myboard[9];
    bool checkwin()
    {
        bool win=true;
        for(int a=0;a<8;a++)
        {
            if(myboard[a]!=a+1)
            {
                win=false;
            }
        }
        win=win&&(myboard[8]==0);
        return win;
    }
    bool isCompleteBoard()
    {
        bool valid=true;
        for(int i=0;i<9;i++)
        {
            if(myboard[i]==9)
            {
                valid=false;
            }
        }
        return valid;
    }
    bool upable()
    {
        if(getposition()>3)
        {return true;}
        else
        {return false;}
    }
    bool downable()
    {
        if(getposition()<7)
        {return true;}
        else
        {return false;}
    }
    bool leftable()
    {
        if((getposition()%3)!=1)
        {return true;}
        else
        {return false;}
    }
    bool rightable()
    {
        if((getposition()%3)!=0)
        {return true;}
        else
        {return false;}
    }
    bool completed()
    {
        bool valid=false;
        if(isAlreadyThere(0)&&isAlreadyThere(1)&&isAlreadyThere(2)&&isAlreadyThere(3)&&isAlreadyThere(4)&&isAlreadyThere(5)&&isAlreadyThere(6)&&isAlreadyThere(7)&&isAlreadyThere(8))
        {valid=true;}
        return valid;
    }
    bool isAlreadyThere(int num)
    {
        bool valid=false;
        for(int i=0;i<9;i++)
        {
            if(myboard[i]==num)
            {
                valid=true;
            }
        }
        return valid;
    }
    int getdifferences()
    {
        int count=0;
        for(int i=0;i<8;i++)
        {
            if(myboard[i]!=i+1){count++;}
        }
        if(myboard[8]!=0){count++;}
        return count;
    }
    int getposition()
    {
        for(int a=0;a<9;a++)
        {
            if(myboard[a]==0)
            {
                return a+1;
            }
        }
        return 0;
    }
    int getLastEmptyindex()
    {
        int idx=0;
        for(int i=0;i<9;i++)
        {
            if(myboard[i]==9)
            {
                idx=i;
                break;
            }
        }
        return idx;
    }
    void clearboard()
    {
        for(int i=0;i<9;i++)
        {
            myboard[i]=9;
        }
    }
    void moveup()
    {
        int temp;
        int pos=getposition();
        temp=myboard[pos-1];
        myboard[pos-1]=myboard[pos-4];
        myboard[pos-4]=temp;
    }
    void movedown()
    {
        int temp;
        int pos=getposition();
        temp=myboard[pos-1];
        myboard[pos-1]=myboard[pos+2];
        myboard[pos+2]=temp;
    }
    void moveleft()
    {
        int temp;
        int pos=getposition();
        temp=myboard[pos-1];
        myboard[pos-1]=myboard[pos-2];
        myboard[pos-2]=temp;
    }
    void moveright()
    {
        int temp;
        int pos=getposition();
        temp=myboard[pos-1];
        myboard[pos-1]=myboard[pos];
        myboard[pos]=temp;
    }
    void drawboard()
    {
        cout<<"-------"<<endl;
        cout<<"|"<<myboard[0]<<"|"<<myboard[1]<<"|"<<myboard[2]<<"|"<<endl;
        cout<<"|"<<myboard[3]<<"|"<<myboard[4]<<"|"<<myboard[5]<<"|"<<endl;
        cout<<"|"<<myboard[6]<<"|"<<myboard[7]<<"|"<<myboard[8]<<"|"<<endl;
        cout<<"-------"<<endl;
        cout<<"\n"<<endl;
        cout<<"Differences : "<<getdifferences()<<endl;
    }
    void setinitialeasy()
    {
        myboard[0]=1;
        myboard[1]=2;
        myboard[2]=3;
        myboard[3]=0;
        myboard[4]=5;
        myboard[5]=6;
        myboard[6]=4;
        myboard[7]=7;
        myboard[8]=8;
    }
    void setInitial()
    {
        for(int i=0;i<8;i++){myboard[i]=i+1;}
        myboard[8]=0;
    }
    void randomizeboard()
    {
        setInitial();
        random_shuffle(&myboard[0],&myboard[8]);
    }
};

int main()
{
    board b;
    char confirm='y';
    while ((confirm=='y')||(confirm=='Y'))
    {
        b.setinitialeasy();
        b.drawboard();
        while (!b.checkwin())
        {
            switch(getch())
            {
                case 72 :
                            //cout<<"UP"<<endl;
                            if(b.upable())
                            {
                                system("cls");
                                b.moveup();
                                b.drawboard();
                            }
                            break;
                case 80 :
                            //cout<<"DOWN"<<endl;
                            if(b.downable())
                            {
                                system("cls");
                                b.movedown();
                                b.drawboard();
                            }
                            break;
                case 75 :
                            //cout<<"LEFT"<<endl;
                            if(b.leftable())
                            {
                                system("cls");
                                b.moveleft();
                                b.drawboard();
                            }
                            break;
                case 77 :
                            //cout<<"RIGHT"<<endl;
                            if(b.rightable())
                            {
                                system("cls");
                                b.moveright();
                                b.drawboard();
                            }
                            break;
            }

        }
        if(b.checkwin())
        {
            cout<<"Congratulation!!! You are the winner!!! Great game!!! :)"<<endl;
        }
        cout<<"\n\n"<<endl;
        cout<<"Anda mau lagi? (y/n)"<<endl;
        cin>>confirm;
        system("cls");
    }

    return 0;
}
