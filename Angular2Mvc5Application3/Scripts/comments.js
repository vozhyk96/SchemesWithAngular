angular.module('Comments', [])
  .controller('CommentsController', ["$scope", function ($scope, $interval) {
      $scope.textcomment = {};
      $scope.comments = [];
      $scope.textcomment = '';

      addLike = function (id) {
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
                  $scope.comments = response
                  $scope.$apply();
              },

          });

      }
      setInterval(getComments, 5000);
      getComments();

      $scope.postComment = function () {
          var data = new FormData();
          data.append("text", $scope.textcomment)
          data.append("url", document.URL)
          $.ajax({
              type: "POST",
              url: "/Post/AddComment",
              processData: false,
              contentType: false,
              data: data,
              success: function (response) {
                  $scope.comments.unshift(response);
                  $scope.$apply();
              },

          });
          $scope.textcomment = '';
          //

      }
  }]);

     
    
      

