﻿using System;
using System.Net.Http;
using Newtonsoft.Json;
using Safe2Pay.Core;

namespace Safe2Pay
{
    public class Marketplace
    {
        /// <summary>
        /// Construtor para as funções de transações
        /// </summary>
        /// <param name="config">Dados de autenticação</param>
        public Marketplace(Config config) => Client = new Client().Create(false, config);

        private Client Client { get; set; }

        /// <summary>
        /// Chamada para criar uma nova subconta.
        /// </summary>
        /// <param name="merchant"></param>
        /// <returns></returns>
        public object New(object merchant)
        {
            var response = Client.Post("Marketplace/Add", merchant);

            var responseObj = JsonConvert.DeserializeObject<Response<MarketplaceResponse>>(response);
            if (responseObj.HasError)
                throw new Exception($"Erro {responseObj.ErrorCode} - {responseObj.Error}");

            return responseObj.ResponseDetail;
        }

        public object Get(object id)
        {
            var query = id is int
                ? $"Id={id}"
                : new FormUrlEncodedContent(id.ToQueryString()).ReadAsStringAsync().Result;

            var response = Client.Get($"Marketplace/Get?{query}");

            var responseObj = JsonConvert.DeserializeObject<Response<MarketplaceResponse>>(response);
            if (responseObj.HasError)
                throw new Exception($"Erro {responseObj.ErrorCode} - {responseObj.Error}");

            return responseObj.ResponseDetail;
        }

        public object Update(object merchant, object id)
        {
            var query = id is int
                ? $"Id={id}"
                : new FormUrlEncodedContent(id.ToQueryString()).ReadAsStringAsync().Result;

            var response = Client.Put($"Marketplace/Update?{query}", merchant);

            var responseObj = JsonConvert.DeserializeObject<Response<MarketplaceResponse>>(response);
            if (responseObj.HasError)
                throw new Exception($"Erro {responseObj.ErrorCode} - {responseObj.Error}");

            return responseObj.ResponseDetail;
        }

        public object List()
        {
            var query = new Filter<Merchant> { PageNumber = 1, RowsPerPage = 100 };
            var encodedQuery = new FormUrlEncodedContent(query.ToQueryString());
            var queryString = encodedQuery.ReadAsStringAsync().Result;

            var response = Client.Get($"Marketplace/List?{queryString}");

            var responseObj = JsonConvert.DeserializeObject<Response<Object<MarketplaceResponse>>>(response);
            if (responseObj.HasError)
                throw new Exception($"Erro {responseObj.ErrorCode} - {responseObj.Error}");

            return responseObj.ResponseDetail.Objects;
        }

        public bool Delete(object id)
        {
            var query = id is int
                ? $"Id={id}"
                : new FormUrlEncodedContent(id.ToQueryString()).ReadAsStringAsync().Result;

            var response = Client.Delete($"Marketplace/Delete?{query}");

            var responseObj = JsonConvert.DeserializeObject<Response<object>>(response);
            if (responseObj.HasError)
                throw new Exception($"Erro {responseObj.ErrorCode} - {responseObj.Error}");

            return (bool)responseObj.ResponseDetail;
        }
    }
}