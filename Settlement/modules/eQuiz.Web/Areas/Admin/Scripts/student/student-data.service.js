(function (angular) {

    angular
        .module("settlementModule")
        .factory("studentDataService", studentDataService);

    studentDataService.$inject = ["$http"];

    function studentDataService($http) {

        var service = {
            getStudentInfo: getStudentInfo,
            saveProfileInfo: saveProfileInfo,
            getViolations: getViolations
        };

        return service;

        function getStudentInfo(studentId) {
            var promise = $http({
                url: '/StudentInfo/GetStudentInfo',
                method: "GET",
                params: { id: studentId }
            });
            return promise;
        }
        function getStudentInfoMock(studentId) {
            return {
                firstName : 'John',
                lastName : 'Smith',
                institute: 'IKTA',
                studyGroup: 'ZI-31',
                paymentTill: '15.08.2017',
                paidSum: 200,
                hostel: 8,
                room: 217,
                violations: ['Smoking', 'Unappropriate behavoir']
            }
        }

        function getViolations() {
            var promise = $http({
                url: '/StudentInfo/GetViolationsList',
                method: "GET",
            });
            return promise;
        }

        function getViolationsMock() {

            return [
                'Smoking',
                'Drinking',
                'Unappropriate behavior',
                'Affray'
            ];
        }

        function saveProfileInfo(studentInfo) {
            var promise = $http({
                url: '/Admin/Student/UpdateUserInfo',
                method: "POST",
                params: { studentInfo: studentInfo }
            });
            //var promise = $http.post("/Admin/Student/UpdateUserInfo", { Id: id, firstName: firstName, lastName: lastName, phone: phone });
            return promise;
        }
    }

})(angular);