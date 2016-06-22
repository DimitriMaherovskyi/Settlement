(function (angular) {

    angular
        .module("settlementModule")
        .factory("hostelDataService", hostelDataService);

    studentDataService.$inject = ["$http"];

    function studentDataService($http) {

        var service = {
            getHostelInfo: getHostels,
            saveHostelProfileInfo: saveHostelProfileInfo,
        };

        return service;

        function getHostels() {
            var promise = $http({
                url: '/HostelInfo/GetHostels',
                method: "GET",
            });
            return promise;
        }

        function saveProfileInfo(hostelInfo) {
            var promise = $http({
                url: '/HostelInfo/UpdateHostelInfo',
                method: "POST",
                params: { hostelInfo: hostelInfo }
            });
            //var promise = $http.post("/Admin/Student/UpdateUserInfo", { Id: id, firstName: firstName, lastName: lastName, phone: phone });
            return promise;
        }
    }

})(angular);