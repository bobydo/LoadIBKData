using IBApi;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
