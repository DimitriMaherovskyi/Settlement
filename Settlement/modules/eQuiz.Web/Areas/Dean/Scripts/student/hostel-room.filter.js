(function (angular) {
    angular.module('settlementModule').filter('hostel', function () {
        return function (input, hostelId) {
            if (!input) { return; }
            var result = input.filter(function (v) {
                return v.HostelId === hostelId;
            });
            return result;
        };
    });
})(angular);