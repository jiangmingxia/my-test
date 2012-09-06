using System;


namespace Test1
{
    class Result:Exception
    {
        internal const int PASS = 1;
        internal const int FAIL = -1;
        internal const int UNRESOLVED = -2;

        private int status;
        private string status_string;

        internal int Status
        {
            get {return status;}
            set 
            {
                status = value;
                if (status == PASS) status_string = "PASS";
                if (status == FAIL) status_string = "FAIL";
                if (status != PASS && status != FAIL)
                {
                    status = UNRESOLVED;
                    status_string = "UNRESOLVED";
                }
            }
        }
        internal string Status_string
        {
            get { return status_string;}        
        }

        internal Result(int i)
        {
            if (i == PASS) { Status = PASS; return; }
            if (i == FAIL) { Status = FAIL; return; }
            Status = UNRESOLVED;
        }
    }
}
