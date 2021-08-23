namespace cs_ProxyGetterChecker {

    enum TypeProxy { HTTP, HTTPS, SOCKS4, SOCKS5, UNDEF };

    class Proxy {

        public Proxy(string ip, string port, string country, TypeProxy typeProxy) {
            this.ip = ip;
            this.port = port;
            this.country = country;
            this.typeProxy = typeProxy;
        }
        //*******************************************************************//
        private string ip { set; get; }
        public string Ip { get => ip; }
        //*******************************************************************//
        private string port { set; get; }
        public string Port { get => port; }
        //*******************************************************************//
        private string country { set; get; }
        public string Country { get => country; }
        //*******************************************************************//
        private TypeProxy typeProxy { set; get; }
        public TypeProxy TypeProxy { get => typeProxy; }
        //*******************************************************************//

    }
}
