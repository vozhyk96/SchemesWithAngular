angular.module('todoApp', [])
  .controller('TodoListController', ["$scope",function ($scope,$interval) {
      $scope.todoList = {};
      $scope.todoList.todos = [];

      addLike = function (id) {
          alert(id.toString());
          var Data = new FormData();
          Data.append("id", id)
          $.ajax({
              type: "POST",
              url: "/Post/AddLike",
              processData: false,
              contentType: false,
              data: Data,

          })
      };

      var getComments = function () {
          var Data = new FormData();
          Data.append("url", document.URL)
          $.ajax({
              type: "POST",
              url: "/Post/GetComments",
              processData: false,
              contentType: false,
              data: Data,
              success: function (response) {
                  console.log("reload comments");
                  $scope.todoList.todos = response
                  $scope.$apply();
              },

          });

      }
      setInterval(getComments, 5000);
      getComments();

      $scope.postComment = function () {
          var data = new FormData();
          data.append("text", $scope.todoList.todoText)
          data.append("url", document.URL)
          $.ajax({
              type: "POST",
              url: "/Post/AddComment",
              processData: false,
              contentType: false,
              data: data,
              success: function (response) {
                  $scope.todoList.todos.unshift(response);
                  $scope.$apply();
              },

          });
          $scope.todoList.todoText = '';
          //

      }
  }]);

     
    
      

