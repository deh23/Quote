using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Quote
{
  public  class DynamoList<T> : List<T>
    {
        public int LastUploadCount { get; set; }
        public delegate void DynamoPosting();

        public void Add(T item, DynamoPosting callback) {
           if(Count > 100)
            {
                callback.Invoke();
            }


        } 
        
    }
}
