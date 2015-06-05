namespace SlimsteMens.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Add_Indexes : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.GalleryQuestions", "Gallery_Id");
            CreateIndex("dbo.PuzzleQuestions", "Puzzle_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PuzzleQuestions", new[] { "Puzzle_Id" });
            DropIndex("dbo.GalleryQuestions", new[] { "Gallery_Id" });
        }
    }
}
