// ======================================================================
//  
//          Copyright (C) 2016-2020 湖南心莱信息科技有限公司    
//          All rights reserved
//  
//          filename : TicketApi.cs
//          description :
//  
//          created by 李文强 at  2016/09/23 17:10
//          Blog：http://www.cnblogs.com/codelove/
//          GitHub ： https://github.com/xin-lai
//          Home：http://xin-lai.com
//  
// ======================================================================

using System;
using System.Collections.Generic;

namespace Magicodes.WeChat.SDK.Apis.Ticket
{
    /// <summary>
    ///  获取AccessToken
    /// </summary>
    public class TicketApi : ApiBase
    {
        private const string ApiName = "ticket";

        /// <summary>
        ///     获取AccessToken
        /// </summary>
        /// <returns></returns>
        public TicketApiResult Get()
        {
            var url = GetAccessApiUrl("getticket", ApiName, ApiRoot, new Dictionary<string, string>
            {
                {"type", "jsapi"}
            });
            var result = Get<TicketApiResult>(url);
            result.ExporesTime = DateTime.Now.AddSeconds(result.Expires - 30);
            return result;
        }

        /// <summary>
        ///     安全获取AccessToken
        /// </summary>
        /// <returns></returns>
        public TicketApiResult SafeGet()
        {
            var appConfig = AppConfig;
            TicketApiResult ticket;
            if (WeChatConfigManager.Current.TicketConcurrentDictionary.ContainsKey(appConfig.AppId))
            {
                ticket = WeChatConfigManager.Current.TicketConcurrentDictionary[appConfig.AppId];
                if (DateTime.Now < ticket.ExporesTime)
                    return ticket;
                ticket = Get();
                WeChatConfigManager.Current.TicketConcurrentDictionary.AddOrUpdate(appConfig.AppId, ticket,
                    (tKey, existingVal) => ticket);
                return ticket;
            }
            ticket = Get();
            WeChatConfigManager.Current.TicketConcurrentDictionary.AddOrUpdate(appConfig.AppId, ticket,
                (tKey, existingVal) => ticket);
            return ticket;
        }

        /// <summary>
        /// 获取JSSDK卡券Ticket
        /// </summary>
        /// <returns></returns>
        public TicketApiResult GetCardAccessToken()
        {
            var url = GetAccessApiUrl("getticket", ApiName, ApiRoot, new Dictionary<string, string>
            {
                {"type", "wx_card"}
            });
            var result = Get<TicketApiResult>(url);
            result.ExporesTime = DateTime.Now.AddSeconds(result.Expires - 30);
            return result;
        }

        /// <summary>
        /// 卡券访问接口票据
        /// </summary>
        /// <returns></returns>
        public TicketApiResult GetSafeCardAccessToken()
        {
            var appConfig = AppConfig;
            TicketApiResult ticket;
            if (WeChatConfigManager.Current.CardTicketConcurrentDictionary.ContainsKey(appConfig.AppId))
            {
                ticket = WeChatConfigManager.Current.CardTicketConcurrentDictionary[appConfig.AppId];
                if (DateTime.Now < ticket.ExporesTime)
                    return ticket;
                ticket = GetCardAccessToken();
                WeChatConfigManager.Current.CardTicketConcurrentDictionary.AddOrUpdate(appConfig.AppId, ticket,
                    (tKey, existingVal) => ticket);
                return ticket;
            }
            ticket = GetCardAccessToken();
            WeChatConfigManager.Current.CardTicketConcurrentDictionary.AddOrUpdate(appConfig.AppId, ticket,
                (tKey, existingVal) => ticket);
            return ticket;
        }
    }
}