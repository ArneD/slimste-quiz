namespace SlimsteMens.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotoAnswerToGaleryQuestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GalleryQuestions", "PhotoAnswer", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GalleryQuestions", "PhotoAnswer");
        }
    }
}
