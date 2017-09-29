angular.module('bookApp')
.controller("CreateBookController", CreateBookController)

function CreateBookController($scope, BookService) {
    $scope.PostBook = {};
    $scope.update = function (PostBook) {
        BookService.postBooks(PostBook).then(function (response) {
            alert("Success");
            window.location = "/";
        });
    }


}