using static UDPForward.Models.ConnectionData;
using System.Collections.Concurrent;
using UDPForward.Models;

namespace UDPForward.Services
{
    public class ConnectionService
    {
        private readonly ConcurrentDictionary<string, ConnectionData> _connections = new();

        // Tạo Offer và lưu kết nối
        public string CreateOffer(Offer offer)
        {
            var connectionId = Guid.NewGuid().ToString();
            _connections[connectionId] = new ConnectionData
            {
                ConnectionId = connectionId,
                Offer = offer
            };
            return connectionId;
        }

        // Nhận Offer
        public Offer? GetOffer(string connectionId)
        {
            return _connections.TryGetValue(connectionId, out var connection) ? connection.Offer : null;
        }

        // Nhận Answer (bây giờ là đối tượng chứ không phải chuỗi)
        public bool AddAnswer(string connectionId, Offer answer)
        {
            if (_connections.TryGetValue(connectionId, out var connection))
            {
                connection.Answer = answer;
                return true;
            }
            return false;
        }

        public Offer? GetAnswer(string connectionId)
        {
            return _connections.TryGetValue(connectionId, out var connection) ? connection.Answer : null;
        }

        // Lưu ICE Candidate
        public bool AddCandidate(string connectionId, IceCandidate candidate)
        {
            if (_connections.TryGetValue(connectionId, out var connection))
            {
                connection.Candidates.Add(candidate);
                return true;
            }
            return false;
        }

        // Lấy ICE Candidate
        public List<IceCandidate>? GetCandidates(string connectionId)
        {
            return _connections.TryGetValue(connectionId, out var connection) ? connection.Candidates : null;
        }
    }
}
