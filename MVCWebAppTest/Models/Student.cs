namespace MVCWebAppTest.Models
{
    public class Student
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool active { get; set; }

        public Student() {
            this.Id = 0;
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.active = false;
        }


        public override string ToString() {
            return $"Student: {this.Id},{this.Name},{this.active}";
        }
    }
}
