angular.module('todoApp', [])
  .controller('TodoListController', function ($interval) {
      var todoList = this;
      todoList.todos = [];
      var Data = new FormData();
      $interval(todoList.addTodo, 10);
      Data.append("url", document.URL)
      $.ajax({
          type: "POST",
          url: "/Post/GetComments",
          processData: false,
          contentType: false,
          data: Data,
          success: function (response) {
              todoList.todos = response
          },

      });

      todoList.addTodo = function () {
          var Data = new FormData();
          Data.append("text", todoList.todoText)
          Data.append("url", document.URL)
          $.ajax({
              type: "POST",
              url: "/Post/AddComment",
              processData: false,
              contentType: false,
              data: Data,
              success: function (response) {
                  todoList.todos.unshift(response)
              },
              
          });
          //todoList.todos.push({ text: todoList.todoText});
          todoList.todoText = '';
          
      };

      

      
  });