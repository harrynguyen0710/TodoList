namespace TodoList.Models
{
    public class Repository
    {
        private static List<Todo> _todoList = new List<Todo>();
        public static IEnumerable<Todo> TodoList 
        {
            get {  return _todoList; } 
        }

        public static void AddTodo(Todo todo)
        {
            _todoList.Add(todo);
        }

        public static void RemoveTodo(Todo todo)
        {
            _todoList.Remove(todo);
        }
        public static void isCompletedTask(string title)
        {
            var task = _todoList.FirstOrDefault(t => t.Equals(title));
            if (task != null)
            {
                task.isCompleted = true;
            }
        }
    }
}
