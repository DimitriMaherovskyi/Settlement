(function (angular) {

    angular
        .module("settlementModule")
        .factory("quoteReviewDataService", quoteReviewDataService);

    quoteReviewDataService.$inject = ["$http"];

    function quoteReviewDataService($http) {

        var service = {
            getQuotes: getQuotesAjaxMock,
            changeQuote: changeQuote
        };

        return service;

        function changeQuote(userId, newValue) {
            var promise = $http({
                url: '/QuotesReview/ChangeQuote',
                method: "GET",
                params: { userId: userId, newValue: newValue}
            });
            return promise;
        }

        function getQuotesAjax() {
            var promise = $http.get('/QuotesReview/GetQuotes');
            return promise;
        }

        function getQuotesAjaxMock() {
            return [
                {
                    UserId: 1,
                    Name: 'John Smith',
                    Status: 'Admin',
                    Institute: 'IKTA, IKNI',
                    Quote: 200
                },
                {
                    UserId: 2,
                    Name: 'Jane Smith',
                    Status: 'Warden',
                    Institute: 'IARX',
                    Quote: 100
                },
                {
                    UserId: 3,
                    Name: 'Jack Brown',
                    Status: 'Dean',
                    Institute: 'INEM',
                    Quote: 50
                }
            ]
        }
    }

})(angular);