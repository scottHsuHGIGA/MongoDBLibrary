"# MongoDBLibrary" 
此套建提供與Linq類似的CRUD語法供使用。

1.下載套件 「MongoDBLibrary
![image](https://user-images.githubusercontent.com/76547233/153969940-95da8c12-4a75-4232-910a-e2f71ce93ca2.png)

2.在需要使用 MongoDB 的地方 new 一個 MongoDBService出來 一連線字串連線到 有權限的 MongoDB Server 資料庫
![image](https://user-images.githubusercontent.com/76547233/153970169-6a8169ac-a074-4cf8-ad49-5d9b92e9c4be.png)

3.CRUD介紹
在此提供與Linq類似語法的 CRUD 擴充方法，較為不同的是並沒有 SaveChange 的概念，使用方法後會直接對資料庫做修改。

*新增
--範例
  ![image](https://user-images.githubusercontent.com/76547233/153970390-75cd9d7b-184a-4799-8c15-3ccdf1a0f78d.png)  
  ![image](https://user-images.githubusercontent.com/76547233/153970441-97a50e5c-3056-4afa-9e80-2b5ce4063196.png)
   使用 MongoDBService 的 GetCollection方法 依 tableName 與 table的schema建一個對應的模型 去DB Server將資料撈回來。
   ps:ViewModel一定要在 schema 對應是 pk 的那個屬性欄位上個 Attribute [BsonId]。
      若 DB server對應資料表的 field 名稱 與 ViewModel對應的名稱不同 可以使用  Attribute [BsonElement(“”)]來對應。
      ![image](https://user-images.githubusercontent.com/76547233/153971409-eaf55d69-d834-4197-8bf1-aab05e31745c.png)
      
*查詢
--範例
  ![image](https://user-images.githubusercontent.com/76547233/153972263-99e0d079-f19c-4a72-b111-7d92d9bce74e.png)
   使用 MongoDBService 的 GetQueryable方法 依 tableName 與 table的schema建一個對應的模型 去DB Server將資料撈回來。
   ps:值得注意的是，可以依需求使用不同的 ViewModel 去 同一張 table 取得所需的資料回來就好，但需要注意，若使用的ViewModel,欄位與 DB 的 table schema 有所不同， 
      請使用 Attribute [BsonIgnoreExtraElements] 以忽略此Model與DB缺少的field ，並用  Attribute [BsonElement(“”)]來對應不同的欄位名稱
      ![image](https://user-images.githubusercontent.com/76547233/153972210-3c90c619-07d8-405d-8cfc-4f3aeb3fc06b.png)
      -「PersonModel」
      ![image](https://user-images.githubusercontent.com/76547233/153972307-dcb40564-a652-433c-8da8-0cee828c8f4d.png)
      -「NameModel」
      ![image](https://user-images.githubusercontent.com/76547233/153972340-0423012f-04dd-4a39-9eca-8df631dca6f2.png)
   
   -Join查詢範例   
   ![image](https://user-images.githubusercontent.com/76547233/153972843-e5d4becb-c7fa-4e41-80d4-10be71b90d40.png)


*更新
取得實體資料後，對資料做修改，使用 MongoDBService 的 Update() UpdateRange() 方法對DB資料做修改動作。
--範例
  Update()
  ![image](https://user-images.githubusercontent.com/76547233/153972458-0810e404-7c0d-48e1-941a-3eb064cbcd93.png)
  UpdateRange()
  ![image](https://user-images.githubusercontent.com/76547233/153972479-4bf46605-8390-4b8c-bf61-c822064b0153.png)

*刪除
取得實體資料後，對資料做修改，使用 MongoDBService 的 Remove() RemoveRange() 方法對DB資料做刪除動作。
--範例
  Remove()
  ![image](https://user-images.githubusercontent.com/76547233/153972748-846a320b-5a4d-4761-9f0a-136edd610664.png)
  RemoveRange()
  ![image](https://user-images.githubusercontent.com/76547233/153972766-0aaaa4c4-8bdc-4192-9a08-2e5ae6e9d0d4.png)

4.修改 從MongoDB 取得的資料庫
使用 MongoDBService 的 GetDatabase("dbName") 方法，若權限許可，可以變更目前連線的資料庫。
![image](https://user-images.githubusercontent.com/76547233/153973034-45052c3e-82e4-4cc1-a361-b9e2616437a6.png)


