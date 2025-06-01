import React from 'react';
import { Plane } from 'lucide-react';

const SeatSelectionHeader = ({ segment }) => (
  <div className="bg-white shadow-sm border-b">
    <div className="max-w-7xl mx-auto px-4 py-4">
      <div className="flex items-center justify-between">
        <div className="flex items-center space-x-4">
          <Plane className="w-8 h-8 text-blue-600" />
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Seat Selection</h1>
            <p className="text-gray-600">Choose your preferred seat</p>
          </div>
        </div>
        <div className="text-right">
          <p className="text-lg font-semibold text-gray-900">
            {segment.flight.airlineCode} {segment.flight.flightNumber}
          </p>
          <p className="text-gray-600">
            {segment.origin} → {segment.destination}
          </p>
        </div>
      </div>
    </div>
  </div>
);

export default SeatSelectionHeader;