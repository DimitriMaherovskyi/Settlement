(function (angular) {

    angular
        .module("settlementModule")
        .factory("studentDataService", studentDataService);

    studentDataService.$inject = ["$http"];

    function studentDataService($http) {

        var service = {
            getStudentInfo: getStudentInfo,
            saveProfileInfo: saveProfileInfo,
            getViolations: getViolations,
            getHostels: getHostels,
            getRooms: getRooms,
            checkIn: checkIn,
            checkOut: checkOut
            
        };

        return service;

        function getHostels() {
            var promise = $http({
                url: '/StudentInfo/GetHostels',
                method: "GET",
            });
            return promise;
        }

        function getRooms() {
            var promise = $http({
                url: '/StudentInfo/GetRooms',
                method: "GET",
            });
            return promise;
        }

        function checkIn(studentId, roomId) {
            var promise = $http({
                url: '/StudentInfo/CheckIn',
                method: "GET",
                params: { id: studentId, room: roomId }
            });
            return promise;
        }

        function checkOut(studentId) {
            var promise = $http({
                url: '/StudentInfo/CheckOut',
                method: "GET",
                params: { id: studentId }
            });
            return promise;
        }

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