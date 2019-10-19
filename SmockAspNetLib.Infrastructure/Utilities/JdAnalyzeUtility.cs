using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmockAspNetLib.Infrastructure.Utilities
{
    public class JdAnalyzeUtility
    {
        public static JdAnalyzeUtility_UnionTag GetUnionTag(string unionTag)
        {
            if (unionTag == null)
                throw new Exception("unionTag not be null");

            var chars = unionTag.Reverse().ToArray();//翻转

            var result = new JdAnalyzeUtility_UnionTag();

            if (chars.Length == 8)
            {
                result.IsRedEnvelope = chars[0] == '1' ? true : false;//第一位
                result.IsCombinationPopularize = chars[1] == '1' ? true : false;//第二位
                result.IsPinGou= chars[2] == '1' ? true : false;//第三位
                result.IsFirstPurchase = chars[3] == '1' && chars[4] == '1' ? true : false;
                //result.IsCashGift = chars[8] == '1' ? true : false;


            }
            else if (chars.Length == 16)
            {
                result.IsRedEnvelope = chars[0] == '1' ? true : false;//第一位
                result.IsCombinationPopularize = chars[1] == '1' ? true : false;//第二位
                result.IsPinGou = chars[2] == '1' ? true : false;//第三位
                result.IsFirstPurchase = chars[3] == '1' && chars[4] == '1' ? true : false;

                result.IsCashGift = chars[8] == '1' ? true : false;
                result.IsUnionCashGift = chars[9] == '1' ? true : false;
                result.IsMarketerCashGift = chars[10] == '1' ? true : false;
            }

            return result;
        }
    }

    public class JdAnalyzeUtility_UnionTag
    {
        /// <summary>
        /// 红包
        /// </summary>
        public bool IsRedEnvelope { get; set; }

        /// <summary>
        /// 是否是组合推广
        /// </summary>
        public bool IsCombinationPopularize { get; set; }

        /// <summary>
        /// 是否拼购
        /// </summary>
        public bool IsPinGou { get; set; }

        /// <summary>
        /// 是否首购
        /// </summary>
        public bool IsFirstPurchase { get; set; }

        /// <summary>
        /// 是否是礼金
        /// </summary>
        public bool IsCashGift { get; set; }

        /// <summary>
        /// 联盟礼金
        /// </summary>
        public bool IsUnionCashGift { get; set; }

        /// <summary>
        /// 推客礼金
        /// </summary>
        public bool IsMarketerCashGift { get; set; }

        //public bool IsUnionCashGift { get; set; }
    }
}
