import React, { useState, useEffect } from "react";
import axios from 'axios';
import './viewAllBookings.css';

function ViewAllBookings() {
  const [packages, setPackages] = useState([]);

  const getAllBookings = async () => {
    try {
      const response = await axios.get(`https://localhost:7169/api/User/allBookings`);
      setPackages(response.data);
      console.log(`Response------>${response.data}`);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  useEffect(() => {
    getAllBookings();
  }, []);

  const handleReset = async (recId) => {
    try {
      const response = await axios.put(`https://localhost:7169/api/User/resetBookStatus/${recId}`);
      if (response.status === 200) {
        alert('Reset success..!!');
        getAllBookings(); 
      } else {
        alert('Failed to reset..!!');
      }
    } catch (err) {
      console.log(`Error while reset..${err}`);
      alert('Failed to reset..!!');
    }
  };

  return (
    <>
      <div className="container">
        <div className="row">
          <h1 className="main_ttl mt-4" style={{ textAlign: 'center', fontWeight: 'bold' }}>All Bookings</h1>
          {packages.map((pack) => (
            <div key={pack.bookingid} className="col-md-3 mb-4 mt-4">
              <div className="card card1">
                <div className="card-body">
                  <h3 className="card-title rec_Name main_ttl">{pack.custname}</h3>
                  <p className="card-text expl main_ttl">Table Name: {pack.tablename}</p>
                  <p className="card-text expl main_ttl">Booking ID: {pack.bookingid}</p>
                </div>
                <div className='card-body'>
                  <button
                    className="btn book-now-btn mt-2"
                    onClick={() => handleReset(pack.tableid)}
                  >
                    Reset
                  </button>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </>
  );
}

export default ViewAllBookings;
