(function (angular) {

    angular
        .module("settlementModule")
        .factory("autoSettlementDataService", autoSettlementDataService);

    autoSettlementDataService.$inject = ["$http"];

    function autoSettlementDataService($http) {

        var service = {
            getStudentsToSettle: getStudentsToSettleAjax,
            getSettleResult: getSettleResultAjax,
            settleStudents: settleStudents,
            discardChanges: discardChanges,
            saveChanges: saveChanges
        };

        return service;

        function getStudentsToSettleAjax() {
            var promise = $http.get('/Settle/GetStudentsToSettle');          
            return promise;
        }

        function getSettleResultAjax() {
            var promise = $http.get('/Settle/GetSettleResult');
            return promise;
        }

        function settleStudents() {
            var promise = $http.post('/Settle/SettleStudents');
            return promise;
        }

        function discardChanges() {
            var promise = $http.post('/Settle/DiscardChanges');
            return promise;
        }

        function saveChanges() {
            var promise = $http.post('/Settle/SaveChanges');
            return promise;
        }
    }

})(angular);