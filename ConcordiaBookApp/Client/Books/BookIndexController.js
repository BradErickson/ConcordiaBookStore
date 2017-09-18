angular.module('bookApp')
.controller("BookIndexController", BookIndexController)

function BookIndexController($scope, BookService) {
    getBooks();
    function getBooks() {
        var book = BookService.getBooks().then(function (data) {
            $scope.books = data;
        });
    }

}