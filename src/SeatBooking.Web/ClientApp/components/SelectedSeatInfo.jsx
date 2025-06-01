import React from 'react';

const SelectedSeatInfo = ({
  selectedSeat,
  getSeatCurrency,
  getSeatPrice,
  onConfirm,
  onCancel
}) => {
  if (!selectedSeat) return null;

  return (
    <div className="mt-6 bg-blue-50 border border-blue-200 rounded-lg p-6">
      <h3 className="text-lg font-semibold text-blue-900 mb-4">Selected Seat</h3>
      <div className="grid grid-cols-2 gap-4">
        <div>
          <p className="text-sm text-gray-600">Seat Number</p>
          <p className="font-semibold text-gray-900">{selectedSeat.code}</p>
        </div>
        <div>
          <p className="text-sm text-gray-600">Price</p>
          <p className="font-semibold text-gray-900">
            {getSeatCurrency(selectedSeat)} {getSeatPrice(selectedSeat)}
          </p>
        </div>
        <div>
          <p className="text-sm text-gray-600">Characteristics</p>
          <p className="font-semibold text-gray-900">
            {selectedSeat.seatCharacteristics?.includes('W') && 'Window, '}
            {selectedSeat.seatCharacteristics?.includes('CH') && 'Extra Legroom, '}
            {selectedSeat.seatCharacteristics?.includes('EL') && 'Emergency Exit, '}
            {selectedSeat.seatCharacteristics?.includes('9') && 'Power Outlet'}
          </p>
        </div>
        <div>
          <p className="text-sm text-gray-600">Status</p>
          <p className="font-semibold text-green-600">
            {selectedSeat.freeOfCharge ? 'Free' : 'Paid Selection'}
          </p>
        </div>
      </div>
      <div className="mt-4 flex space-x-3">
        <button
          className="flex-1 bg-blue-600 text-white py-2 px-4 rounded-lg hover:bg-blue-700 transition-colors"
          onClick={onConfirm}
        >
          Confirm Selection
        </button>
        <button 
          className="px-4 py-2 border border-gray-300 rounded-lg hover:bg-gray-50 transition-colors"
          onClick={onCancel}
        >
          Cancel
        </button>
      </div>
    </div>
  );
};

export default SelectedSeatInfo;