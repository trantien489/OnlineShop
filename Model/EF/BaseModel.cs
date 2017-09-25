using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool Status { get; set; }

        //private DateTime mCreatedDate = new SqlDateTime(DateTime.UtcNow).Value;
        //[DataType(DataType.Date)]
        //public DateTime CreatedDate
        //{
        //    get
        //    {
        //        return mCreatedDate;
        //    }
        //    set
        //    {
        //        mCreatedDate = value;
        //    }
        //}
        public DateTime CreatedDate { get; set; }

        //private DateTime mModifiedDate = new SqlDateTime(DateTime.UtcNow).Value;
        //[DataType(DataType.Date)]
        //public DateTime ModifiedDate
        //{
        //    get
        //    {
        //        return mModifiedDate;
        //    }
        //    set
        //    {
        //        mModifiedDate = value;
        //    }
        //}
        public DateTime ModifiedDate { get; set; }

        

    }
}
