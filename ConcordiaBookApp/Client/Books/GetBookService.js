angular.module('bookApp')
.factory('BookService', BookService)

function BookService ($http) {
    var BookService = {};
    BookService.getBooks = function () {
        return $http({
            method: 'GET',
            url: '/Books/getBooks'
        }).then(function successCallback(response) {
            return response.data;
        }, function errorCallback(response) {
            console.log(response);
        });
    };

    BookService.postBooks = function (PostBook) {
        return $http({
            method: 'POST',
            url: '/Books/postBook',
            data: PostBook,
            headers: {
                "Content-Type": "application/json"
            }
        }).then(function successCallback(response) {
            console.log("response", response);
        }, function errorCallback(response) {
            console.log("response", response);
        });
    }

    BookService.deleteBooks = function (bookId) {
        return $http({
            method: 'DELETE',
            url: '/Books/deleteBook/' + bookId
        }).then(function successCallback(response) {
            console.log("response", response);
        }, function errorCallback(response) {
            console.log("response", response);
        });
    }

    BookService.rentBook = function (bookId) {
        return $http({
            method: 'DELETE',
            url: '/Books/RentBook/' + bookId
        }).then(function successCallback(response) {
            console.log("response", response);
        }, function errorCallback(response) {
            console.log("response", response);
        });
    }

    return BookService;

}