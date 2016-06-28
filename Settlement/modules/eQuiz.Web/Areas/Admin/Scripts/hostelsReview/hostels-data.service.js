(function (angular) {

    angular
        .module("settlementModule")
        .factory("hostelsReviewDataService", hostelsReviewDataService);

    HostelsReviewDataService.$inject = ["$http"];

    function reviewDataService($http) {

        var service = {
            getHostels: getHostelsAjax,
            addHostel: addHostel
        };

        return service;

        function getHostelsAjax() {
            var promise = $http.get('/StudentReview/GetHostelsList');
            return promise;
        }

        function addHostel(newHostel) {
            var promise = $http({
                url: '/HostelsReview/AddHostel',
                method: "POST",
                params: {
                    number: newHostel.number,
                    address: newHostel.address,
                    monthPaymentSum: newHostel.monthPaymentSum
                }
            });
            return promise;
        }
    }

})(angular);