using System;
using System.Collections.Generic;
using System.Text;

namespace FraudDefense.Gateway.IpQualityScore.Contracts
{
    public class EmailInfoResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public bool Valid { get; set; }
        public bool Disposable { get; set; }
        public bool Timed_out { get; set; }
        public string Deliverability { get; set; }
        public bool Suspect { get; set; }
        public int Smtp_score { get; set; }
        public int Overall_score { get; set; }
        public string First_name { get; set; }
        public bool Generic { get; set; }
        public bool Common { get; set; }
        public bool Dns_valid { get; set; }
        public bool Honeypot { get; set; }
        public bool Recent_abuse { get; set; }
        public bool Frequent_complainer { get; set; }
        public string Suggested_domain { get; set; }
        public bool Catch_all { get; set; }
        public string Spam_trap_score { get; set; }
        public string Request_id { get; set; }
        public bool Leaked { get; set; }
    }
}
