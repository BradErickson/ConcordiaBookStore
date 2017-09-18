angular.module('bookApp')
.controller("CreateBookController", CreateBookController)

function CreateBookController($scope, BookService) {
    $scope.PostBook = {};
    $scope.update = function (PostBook) {
        debugger;
        BookService.postBooks(PostBook).then(function (response) {
            console.log('post response: ', response)
        });
    }
}