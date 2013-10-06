function BookCtrl($scope, $http) {
    angular.extend($scope, window.bookStoreData);
    $scope.purchaseBook = function (bookId) {
        $http.post(window.bookStoreData.cartUrl, { bookId: bookId })
            .success(function () { alert('book added to cart'); });
    };
}
BookCtrl.$inject = ['$scope', '$http'];