using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test1.Command;


namespace Test1.QC
{
    class SampleTest
    {
        
        

        private static void prepare()
        {

            Project p = Resource.createProject();
                
        }


        public static void Main(string[] args)
        {
            string a = "setup -testset testset.txt -config config.txt";
            string[] bs = a.Split(' ');

            //load command
            if (!CommandReader.load(bs)) return;


            //init current state

            //get commander
            ICommand command = CommandFactory.getCommand(CommandReader.Command);
            
            


            int interval = 20;
            Thread.Sleep(interval * 1000);
            
            //Resource.load();
            //prepare();
            //Resource.finishLoad();

           
            //Resource.release();
        }

    }
}
