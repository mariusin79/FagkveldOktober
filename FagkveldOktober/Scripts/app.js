(function () {
    var bookstore = angular.module('BookstoreApp', ['ngResource', 'ngRoute']);

    bookstore.config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: '/Scripts/Views/books.html',
                controller: 'BookCtrl',
            })
            .when('/Book/:bookId', {
                templateUrl: '/Scripts/Views/book.html',
                controller: 'PurchaseCtrl',
            })
            .when('/Cart', {
                templateUrl: '/Scripts/Views/cart.html',
                controller: 'CartCtrl'
            }).otherwise({ redirectTo: '/' });

    }]);

    bookstore.factory('Book', ['$resource', function ($resource) {
        var book = $resource('/Home/Books', null, {
            'getAll': { method: 'GET', isArray: true }
        });
        return book;
    }]);

    bookstore.factory('AlsoPurchased', ['$resource', function ($resource) {
        return $resource('/Purchase/Index', null, {           
           'get': {method: 'GET', isArray: true } 
        });
    }]);

    bookstore.factory('Cart', ['$resource', function ($resource) {
        var cart = $resource('/Cart/Index', null, {
            'getContents': { method: 'GET', isArray: true },
            'addToCart': { method: 'PUT' },
            'checkout': { method: 'POST' }
        });
        return cart;
    }]);

    bookstore.controller('BookCtrl', ['$scope', 'Book', 'Cart', function ($scope, Book, Cart) {
        $scope.books = Book.getAll();
    }]);

    bookstore.controller('PurchaseCtrl', ['$scope', '$routeParams', 'AlsoPurchased', 'Book', 'Cart', function ($scope, $routeParams, AlsoPurchased, Book, Cart) {
        $scope.selectedBook = Book.get({ bookId: $routeParams.bookId });
        $scope.alsoPurchased = AlsoPurchased.get({ bookId: $routeParams.bookId });
        $scope.purchaseBook = function (bookId) {
            Cart.addToCart({ bookId: bookId }, function () {
                alert('book added to acart');
            });
        };
    }]);

    bookstore.controller('CartCtrl', ['$scope', '$location', 'Cart', function ($scope, $location, Cart) {
        $scope.cart = Cart.getContents();
        $scope.getSubTotalInOere = function () {
            var i,sum = 0;
            for (i = 0, l = $scope.cart.length; i < l ;i++)
            {
                sum += $scope.cart[i].sumTotalInOere;
            }
            return sum;
        };
        $scope.checkout = function () {
            Cart.checkout(function () { $location.path('#/'); });
        };
    }]);

    bookstore.directive('book', function () {
        return {
            restrict: 'E',
            scope: '@',
            templateUrl: '/Scripts/Views/templates/book-template.html'
        };
    });
})();