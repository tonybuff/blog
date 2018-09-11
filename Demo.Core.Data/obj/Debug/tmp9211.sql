CREATE TABLE [dbo].[UserRole] (
    [Id] [int] NOT NULL IDENTITY,
    [CreateTime] [datetime] NOT NULL,
    [IsDelete] [bit] NOT NULL,
    [TimeSpan] rowversion NOT NULL,
    CONSTRAINT [PK_dbo.UserRole] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Id] ON [dbo].[UserRole]([Id])
CREATE TABLE [dbo].[RoleInfo] (
    [Id] [int] NOT NULL IDENTITY,
    [Description] [nvarchar](max),
    [CreateTime] [datetime] NOT NULL,
    [IsDelete] [bit] NOT NULL,
    [TimeSpan] rowversion NOT NULL,
    [UserRole_Id] [int],
    CONSTRAINT [PK_dbo.RoleInfo] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserRole_Id] ON [dbo].[RoleInfo]([UserRole_Id])
ALTER TABLE [dbo].[Member] ADD [Salt] [nvarchar](max)
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [FK_dbo.UserRole_dbo.Member_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Member] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[RoleInfo] ADD CONSTRAINT [FK_dbo.RoleInfo_dbo.UserRole_UserRole_Id] FOREIGN KEY ([UserRole_Id]) REFERENCES [dbo].[UserRole] ([Id])
INSERT INTO [__MigrationHistory] ([MigrationId], [Model], [ProductVersion]) VALUES ('201502110213513_FirstMigration', 0x1F8B0800000000000400ED5BDD6EDB3614BE1FB0771074B50DA89DA42DD0154E8BD44E8A604D52C46977593012ED10952895A432E7D976B147DA2B8C942D921249FD594E932E3781C59F8F8787E71CF2FCE4DFBFFF99BC5DC591770B0945093EF4F7477BBE07719084082F0FFD8C2D9EBDF2DFBEF9F9A7C97118AFBCCFC5B8E7621C9F89E9A17FC358FA7A3CA6C10D8C011DC52820094D166C1424F11884C9F8606FEFF7F1FEFE1872089F6379DEE432C30CC530FFE09FD3040730651988CE92104674D3CE7BE639AA770E62485310C0437F06E364344D081CCD0003A3D9359FCCE08AF9DE518400A7670EA385EFA52F5E7FA270CE488297F3143004A2ABBB14F2FE058828DC50FF3A7DD176037B076203638071C2385C827B31C0975BE39B3BE64C607782AC7C8387FE198CAF21D1C7F0517FC0BB52036FFA48921412767709179B99A7A1EF8DCBF3C6D589729A36472CCE7F61F6FCC0F7CEB32802D711943CE24C9C33CEEAF7104302180C3F02C620C1622ECC893756ADACC1CF80885FC54AFC40B864F9DE095AC1F003C44B7623573B03ABA2E52517AF4F187139E47318C9A085B8FA853F024AFF4C4878EF0B8BD95FBFCB968F6380A221575D7FD72F3A0711EBB826FFB9E5A25302B9345E21C5E499FCEEC8B4533A83116412E85D924410E0CE3862716E67B0C4411810AE1EC2B46584709B7A7796EF37674E238F5E75D2C66912A71983A60D3807B768995BAB0AB91F9225C2FC0FF5BD4B18E523E80D4AD7D67354F47ED91824EF8424F165126913375D5FAE005942210189BD7F9E642430B9E7244C180C8164A5ABE8B4D055E932E8AAF6DBE89A8C9545AEB5D3C526EFDF52BFCF50D85DC6D3A3302490D28E8ABAFF725B93F4A4A8DB2A6A21E9DB696921ED2E2D2DB4A597364895FD81DE2D4F82BB43C1ED60C6AB82EB32F36DA912734FF122B1DF7B125D1B66A14DF6BAC95343B652AD02E64752AD19A40141E9DA857A7A383E40B57689E711A5498072A5A93C84BE583DD7631C7A0DF7D1FAF8F5DB8CEF268B184A231470120EFDDF0C06BA61E5A34FC11626A60CBA371AED57F7ACEDAE7ED355F3E5A2CE69CB1475EAB5DB445F2D72EB7DEFFB55BB7181D762EC1D052C0FED4C010D40688A2D9789B0DCC24D0D2442E741C4859532021066A65D4238402988EA49AF4C6B69D0045972816ACF0CA6100B9B547F1CFD57960B5418D5C4973EA2A62EA44699B0DC4EC3099C79AF69E0F2B6AA5761E7FED75647C4F338C72091F7459CA8285F954231630E59E9D1C16F6D65C02AFA606CB10CA0F9C50684B2500D2005B76C20EA001A40B4278801A2185D01D1186BEE493EB7B4510E57A22AEBCD965C925FE2A1A134CDB65B0392A7593545E56DB66081F1E23459506BD55BD9758D724D026A58E032E3BB65812658355C70189CB626A73F2F4C0BA36169B4B76648F19A9176456517C6EBF4429186183BF210933390A6FC81AAE525362DDE7C9D94983E9B774F07C46B8C71402D590149AD5C89BFE0C012567AC5132E8427885026D221D740BCF7A6616C0C6B65458BB5CAC6D43CB4C21C15E3C56F65AC6DD999915DAB14274FF8E6627159E72F554303CC897972084480585C986912653176DFDDEED92A5FA163A8D6F6482A01A123A9D60E34A98C428928D5DC1E6B9323D071364DED31D6217F1D62DDD21E4177C3741CBDBD3D9AF2C44AE72E5BDB23295F4C4752AD26D2645C915FE39568688BF1AC2E2B5F2BD55457EC20CA698F67E7784DEAE99EBA1B05D562D92510D5FC2486F72686EA6A1F440CED81E41CAF490CDD537723864FA2B3A5E8D4BD2F7B888E3D50DA4A74DC5377233AA5F8A70E53EA7812C5E145B1EC22D86F54E92176B836E59CB697A3F07B1C09F9C6B8582B89DC80D8E356DAE2DDE97246CD7A6B4A2D419CD521CA0362A754C4B5654CBBD56EAB5E6167A9B039CD1D2E327D5A5BB3633903B79FDDF31824E00002E276DC1F948CD46CB9594A8C10427588B45C32945009194C36EE7B737DA3E1CFAF87F81EDFFE2D0A852F3FBFA30CC66BA19B7F8BA611E2B6520D3803182D206557C957880FFD83BDBD5795E2C81E858B634AC3E861562F22B1F9C6A464D732BE4AE122BE0524B801C4A89EDBB22E71285CA3EC7028E05255610BD0AE458305E42F3158FDBA7D3E37E4DF6C887CEE35625BE77249F2D7A6B4DA29A02A035BBBD623284DCB30FA9641942BDC0289E7D0B6656A56691345695B57A1FDA05262AEA53D8D346359A7648FA0EC6B2706FF7F23248FA0F86827076CA93B7ABA7BFA5995D2637A4BBBD2BFA848F7D43B56FB78B64C74CF72A45E6530CE74D8E0652F35B17D73ADDAB8C17DD6BB0C75F483163C99D9EAFEB5350F5C6A6A42F18FBB3C6A886A28557ED3BDF8A9EFD10F7020AD0FBF26986EAED5104B1A5C0AEC4562661584230B51A94E77D689ADA32EFC4EBF16C7BCBED43AD690D59690D9F09D8553CE0AB3DA0233DB129DCBCF6AABCF6C2B7CC7DAB4F2A1EAD5236D0AD1AC356C0FADF6ACDF16ABE2524E993EE8DAB256243BACB32DDABE93BA3133B8CB0D9CF62FEEDCC252B4541022648D6150326D728C20B330B3158A8A218693CF00F73DC01161680102C6BB034869FE2F159F4194E551BC6B189EE28B8CF1173FDF329799A8F49F1AC252D7AD9F17C795699E5CE4CE141D620B9C4C24DCA70BFC2E435128E93EB1F8AF0E0871056C7C1C71964CF83ACB3B89749EE096401BF6C99BEB0AC669C4C1E8059E835BE8A6AD9987658E4D66082C0988E90643CDE79F5CFCC278F5E63FE5777EDFA4410000, '5.0.0.net45')
