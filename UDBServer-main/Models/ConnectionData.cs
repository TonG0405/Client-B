using System;

namespace UDPForward.Models
{
    public class Offer
    {
        public string Sdp { get; set; } = string.Empty;
        public string Type { get; set; } = "offer"; // or "answer"
    }

    public class IceCandidate
    {
        public string Candidate { get; set; } = string.Empty;
        public string SdpMid { get; set; } = string.Empty;
        public int SdpMLineIndex { get; set; }
    }

    public class ConnectionData
    {
        public string ConnectionId { get; set; } = string.Empty;
        public Offer? Offer { get; set; }
        public Offer? Answer { get; set; }  // Đã chuyển từ string thành Offer
        public List<IceCandidate> Candidates { get; set; } = new List<IceCandidate>();
    }
}
