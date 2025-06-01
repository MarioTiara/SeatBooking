import React from 'react';
const Legend = () => (
  <div className="bg-white rounded-lg shadow-sm border p-6">
    <h3 className="text-lg font-semibold mb-4">Seat Legend</h3>
    <div className="space-y-3">
      <div className="flex items-center space-x-3">
        <div className="w-6 h-6 bg-purple-100 border-2 border-purple-300 rounded-t-lg"></div>
        <span className="text-sm">Premium Seat (Extra Legroom)</span>
      </div>
      <div className="flex items-center space-x-3">
        <div className="w-6 h-6 bg-green-100 border-2 border-green-300 rounded-t-lg"></div>
        <span className="text-sm">Preferred Seat (Window/Aisle)</span>
      </div>
      <div className="flex items-center space-x-3">
        <div className="w-6 h-6 bg-gray-100 border-2 border-gray-300 rounded-t-lg"></div>
        <span className="text-sm">Standard Seat</span>
      </div>
      <div className="flex items-center space-x-3">
        <div className="w-6 h-6 bg-gray-300 border-2 border-gray-400 rounded-t-lg"></div>
        <span className="text-sm">Unavailable</span>
      </div>
      <div className="flex items-center space-x-3">
        <div className="w-6 h-6 bg-blue-600 border-2 border-blue-800 rounded-t-lg"></div>
        <span className="text-sm">Selected</span>
      </div>
    </div>
  </div>
);

export default Legend;
