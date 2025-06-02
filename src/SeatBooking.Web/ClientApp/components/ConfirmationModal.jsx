import React from "react";

const ConfirmationModal = ({ isOpen, onConfirm, onCancel, message }) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
      <div className="bg-white rounded-lg shadow-lg p-6">
        <p className="text-lg mb-4">{message}</p>
        <div className="flex justify-end space-x-4">
          <button className="px-4 py-2 bg-gray-300 rounded" onClick={onCancel}>Cancel</button>
          <button className="px-4 py-2 bg-blue-600 text-white rounded" onClick={onConfirm}>Confirm</button>
        </div>
      </div>
    </div>
  );
};

export default ConfirmationModal;