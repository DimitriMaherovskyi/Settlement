(function (angular) {

    angular
        .module("settlementModule")
        .factory("quoteReviewDataService", quoteReviewDataService);

    reviewDataService.$inject = ["$http"];

    function reviewDataService($http) {

        var service = {
            getQuotes: getQuotesAjax,
            changeQuote: changeQuote
        };

        return service;

        function changeQuote() {
            var promise = $http.get('/QuotesReview/ChangeQuote');
            return promise;
        }

        function getQuotesAjax() {
            var promise = $http.get('/QuotesReview/GetQuotes');
            return promise;
        }
    }

})(angular);