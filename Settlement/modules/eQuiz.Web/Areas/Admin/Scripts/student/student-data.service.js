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
            checkOut: checkOut,
            addPay: addPay,
            addViolation: addViolation          
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
                method: "POST",
                params: { studentId: studentId, roomId: roomId }
            });
            return promise;
        }

        function checkOut(studentId) {
            var promise = $http({
                url: '/StudentInfo/CheckOut',
                method: "POST",
                params: { studentId: studentId }
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

        function saveProfileInfo(id, name, surname, group, institute) {
            var promise = $http({
                url: '/StudentInfo/SaveStudentProfileInfo',
                method: "POST",
                params: { id: id, name: name, surname: surname, studyGroup: group, institute: institute}
            });
            return promise;
        }

        function addPay(sum, studentId, hostelId, dateTill) {
            var promise = $http({
                url: '/StudentInfo/AddPayment',
                method: "POST",
                params: { sum: sum, studentId: studentId, hostelId: hostelId, dateTill: dateTill }
            });
            return promise;
        }

        function addViolation(id, violationId) {
            var promise = $http({
                url: '/StudentInfo/AddViolation',
                method: "POST",
                params: { studentId: id, violationId: violationId }
            });
            return promise;
        }
    }

})(angular);