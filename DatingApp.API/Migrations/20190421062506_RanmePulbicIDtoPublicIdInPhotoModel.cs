using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class RanmePulbicIDtoPublicIdInPhotoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
              @"
              PRAGMA foreign_keys = 0;

              CREATE TABLE Photos_temp AS SELECT *
                                            FROM Photos;
              
              DROP TABLE Photos;
              
            CREATE TABLE Photos (
                Id	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                Url	TEXT,
                Description	TEXT,
                DateAdded	TEXT NOT NULL,
                IsMain		INTEGER NOT NULL,
                UserId		INTEGER NOT NULL,
                PublicId	TEXT,
                CONSTRAINT FK_Photos_Users_UserId FOREIGN KEY(UserId) REFERENCES Users(Id) ON DELETE CASCADE
             );

            INSERT INTO Photos 
              (
                  Id,
                  Url,
                  Description,
                  DateAdded,
                  IsMain,
                  UserId,
                  PublicId
              )
              SELECT Id,
                     Url,
                     Description,
                     DateAdded,
                     IsMain,
                     UserId,
                     PublicId
              FROM Photos_temp;
              
              DROP TABLE Photos_temp;
              
              PRAGMA foreign_keys = 1;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
