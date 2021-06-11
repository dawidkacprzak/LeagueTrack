/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
namespace ApiWrapper.Abstract.RequestLimiter
{
    public interface IRequestLimiterBuilder
    {
        public void BuildRequestLimiter();

        public IRequestLimiter GetRequestLimiter();
    }
}