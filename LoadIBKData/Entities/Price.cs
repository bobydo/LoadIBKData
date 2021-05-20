using IBApi;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadIBKData.Entities
{
    [Table("Prices")]
    public class Price : Bar, IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PriceId")]
        public int Id { get; set; }
        public string Symbol { get; private set; }

        //No suitable constructor found for entity type
        public Price()
        {

        }

        //[Obsolete("This property has been deprecated and should no longer be used.", true)]
        //public override string Time
        //{
        //    get
        //    {
        //        return DateTime.ParseExact(base.Time, "yyyyMMdd  hh:mm:ss", CultureInfo.InvariantCulture).ToString();
        //    }
        //}

        public Price(string symbol, Bar bar)
            : base(bar.Time, bar.Open, bar.High, bar.Low, bar.Close, bar.Volume, bar.Count, bar.WAP)
        {
            this.Symbol = symbol;
        }

        public Price(string symbol, string time, double open, double high, double low, double close, long volume, int count, double wap)
            :base(time, open, high, low, close, volume, count, wap)
        {
            Symbol = symbol;
        }
    }
}
