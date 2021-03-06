﻿using System;
using System.Xml.Serialization;

namespace Magicodes.WeChat.SDK.Pays.Reverse
{
    [Serializable]
    [XmlRoot("xml")]
    public class ReverseRequest
    {
        /// <summary>
        ///     公众账号ID是String(32)wx8888888888888888微信分配的公众账号ID（企业号corpid即为此appId）
        /// </summary>
        [XmlElement("appid")]
        public string Appid { get; set; }

        /// <summary>
        ///     商户号是String(32)1900000109微信支付分配的商户号
        /// </summary>
        [XmlElement("mch_id")]
        public string Mch_id { get; set; }

        /// <summary>
        ///     微信订单号否String(32)1.21775E+27微信的订单号，优先使用
        /// </summary>
        [XmlElement("transaction_id")]
        public string Transaction_id { get; set; }

        /// <summary>
        ///     商户订单号是String(32)1.21775E+27商户系统内部的订单号,transaction_id、out_trade_no二选一，如果同时存在优先级：transaction_id> out_trade_no
        /// </summary>
        [XmlElement("out_trade_no")]
        public string Out_trade_no { get; set; }

        /// <summary>
        ///     随机字符串是String(32)5K8264ILTKCH16CQ2502SI8ZNMTM67VS随机字符串，不长于32位。推荐随机数生成算法
        /// </summary>
        [XmlElement("nonce_str")]
        public string Nonce_str { get; set; }

        /// <summary>
        ///     签名是String(32)C380BEC2BFD727A4B6845133519F3AD6签名，详见签名生成算法
        /// </summary>
        [XmlElement("sign")]
        public string Sign { get; set; }

        /// <summary>
        ///     签名类型否String(32)HMAC-SHA256签名类型，目前支持HMAC-SHA256和MD5，默认为MD5
        /// </summary>
        [XmlElement("sign_type")]
        public string Sign_type { get; set; }
    }
}