import React from "react";
import { User } from "lucide-react";
const PassengerInfo = ({ passenger }) => (
  <div className="bg-white rounded-lg shadow-sm border p-6 mb-6">
    <h3 className="text-lg font-semibold mb-4 flex items-center">
      <User className="w-5 h-5 mr-2 text-blue-600" />
      Passenger
    </h3>
    <div className="space-y-2">
      <p className="font-medium">
        {passenger.passengerDetails.firstName}{" "}
        {passenger.passengerDetails.lastName}
      </p>
      <p className="text-gray-600">
        {passenger.passengerInfo.type === "ADT"
          ? "Adult"
          : passenger.passengerInfo.type}
      </p>
      <p className="text-sm text-gray-500">
        {passenger.passengerInfo.emails[0]}
      </p>
    </div>
  </div>
);


export default PassengerInfo;