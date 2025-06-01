import React from "react";
import { Plane,MapPin  } from "lucide-react";

const FlightInfo = ({ segment, departureInfo, arrivalInfo, aircraft }) => (
  <div className="bg-white rounded-lg shadow-sm border p-6 mb-6">
    <h3 className="text-lg font-semibold mb-4 flex items-center">
      <MapPin className="w-5 h-5 mr-2 text-blue-600" />
      Flight Details
    </h3>
    <div className="space-y-3">
      <div className="flex justify-between">
        <span className="text-gray-600">Flight</span>
        <span className="font-medium">
          {segment.flight.airlineCode}{" "}
          {segment.flight.flightNumber}
        </span>
      </div>
      <div className="flex justify-between">
        <span className="text-gray-600">Route</span>
        <span className="font-medium">
          {segment.origin} → {segment.destination}
        </span>
      </div>
      <div className="flex justify-between">
        <span className="text-gray-600">Date</span>
        <span className="font-medium">{departureInfo.date}</span>
      </div>
      <div className="flex justify-between">
        <span className="text-gray-600">Departure</span>
        <span className="font-medium">{departureInfo.time}</span>
      </div>
      <div className="flex justify-between">
        <span className="text-gray-600">Arrival</span>
        <span className="font-medium">{arrivalInfo.time}</span>
      </div>
      <div className="flex justify-between">
        <span className="text-gray-600">Aircraft</span>
        <span className="font-medium">Boeing {aircraft}</span>
      </div>
      <div className="flex justify-between">
        <span className="text-gray-600">Class</span>
        <span className="font-medium">{segment.cabinClass}</span>
      </div>
    </div>
  </div>
);
export default FlightInfo;
