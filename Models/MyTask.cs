namespace MyTasks.Models
{
    public class MyTask
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public bool IsDone { get; set; }

        public int UserId { get; set; }
        public MyTask(int Id, string Name, bool IsDone)
        {
            this.Id = Id;
            this.Name = Name;
            this.IsDone = IsDone;
        }
    }
}