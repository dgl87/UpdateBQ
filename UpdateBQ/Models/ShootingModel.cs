using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UpdateBQ.Models
{
    [BsonDiscriminator("shooting")]
    [BsonIgnoreExtraElements]
    public class ShootingModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }
        [BsonElement("created")]
        public DateTime Created { get; set; }
        [BsonElement("recipient")]
        public string Recipient { get; set; }
        [BsonElement("kind")]
        public NodeType Kind { get; set; }
        [BsonElement("status")]
        public ShootingStatus Status { get; set; }
        [BsonElement("correlationId")]
        public string CorrelationId { get; set; }
        [BsonElement("contextId")]
        public string ContextId { get; set; }
        [BsonElement("journeyId")]
        public string JourneyId { get; set; }
        [BsonElement("workflowId")]
        public string WorkflowId { get; set; }
        [BsonElement("nodeKey")]
        public string NodeKey { get; set; }
        [BsonElement("scheduleDate")]
        public DateTime? ScheduleDate { get; set; }
        [BsonElement("isTest")]
        public bool IsTest { get; set; }
    }
    public enum ShootingStatus
    {
        Pending,
        Sent,
        Complete
    }
    public enum NodeType
    {
        API = 0,
        Email = 1,
        EmailReceived = 2,
        EmailReplied = 3,
        EmailClicked = 4,
        EmailOpened = 5,
        EmailCertificate = 6,
        EmailCertificateReceived = 7,
        EmailCertificateReplied = 8,
        EmailCertificateClicked = 9,
        EmailCertificateOpened = 10,
        Sms = 11,
        SmsReceived = 12,
        SmsReplied = 13,
        SmsClicked = 14,
        SmsCertificate = 15,
        SmsCertificateReceived = 16,
        SmsCertificateReplied = 17,
        SmsCertificateClicked = 18,
        WhatsApp = 19,
        WhatsAppReceived = 20,
        WhatsAppReplied = 21,
        WhatsAppClicked = 22,
        WhatsAppCertificate = 23,
        WhatsAppCertificateReceived = 24,
        WhatsAppCertificateReplied = 25,
        WhatsAppCertificateClicked = 26,
        Print = 27,
        SendToPrintShop = 28,
        Yes = 29,
        No = 30,
        DigitalSign = 31,
        DocumentGenerate = 32,
        Rcs = 33,
        RcsCompatible = 34,
        RcsReceived = 35,
        RcsReplied = 36,
        RcsClicked = 37,
        RcsRead = 38,
        Push = 39
    }
}