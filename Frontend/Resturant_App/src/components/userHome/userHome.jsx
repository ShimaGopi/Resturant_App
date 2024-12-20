import React, { useState, useEffect } from 'react';
import './userHome.css';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import chairImage from '/public/chair.png';

function UserHome() {
  const userId = localStorage.getItem('userId');
  const [tables, setTables] = useState([]);
  const [filteredTables, setFilteredTables] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const navigate = useNavigate();

  const logOut = () => {
    if (window.confirm('Are you sure want to logout?')) {
      localStorage.clear();
      navigate('/login');
    }
  };

  const getTables = async () => {
    try {
      const response = await axios.get(`https://localhost:7169/api/User/alltables`);
      setTables(response.data);
      setFilteredTables(response.data);
      console.log(`Response------>${response.data}`);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  useEffect(() => {
    getTables();
  }, []);

  useEffect(() => {
    const results = tables.filter(table =>
      table.tablename.toLowerCase().includes(searchTerm.toLowerCase())
    );
    setFilteredTables(results);
  }, [searchTerm, tables]);

  const handleBookNow = async (tableId) => {
    const customerName = prompt('Enter customer name:');
    if (customerName) {
      try {
        const response = await axios.post(`https://localhost:7169/api/User/bookTable`, {
          tableid: tableId,
          custname:customerName
        });
        if (response.status === 200) {
          alert('Your booking is successful!');
          getTables(); 
        } else {
          alert('Failed to book table..!!');
        }
      } catch (err) {
        console.log(`Error while booking table..${err}`);
        alert('Failed to book table..!!');
      }
    }
  };

  return (
    <>
      <header className='bg'>
        <div className='container'>
          <div className='row align-items-center'>
            <div className='col-4'>
              <h2 className='heading'>User Dashboard</h2>
            </div>
            <div className='col-8 btn-align'>
              <button className='btns' onClick={logOut}>LOGOUT</button>
            </div>
          </div>
        </div>
      </header>
      <div className="container mt-4">
        <div className="row">
        </div>
      </div>
      <h1 className='packages-heading mb-4'>Tables Live Now</h1>
      <div className="container">
        <div className="row">
          {filteredTables.map((table) => (
            <div key={table.tableid} className="col-md-4 mb-4">
              <div className={`card card1 ${table.status == 2 ? 'booked-table' : 'available-table'}`}>
                <img src={chairImage} className="card-img-top" alt={table.tablename} />
                <div className="card-body">
                  <h3 className="card-title rec_Name main_ttl">{table.tablename}</h3>
                  <p className={`card-text expl main_ttl ${table.status == 1 ? 'avail' : 'not-avail'}`}>Status: {table.status == 1 ? 'Available' : 'Booked'}</p>
                  {table.status == 1 && (
                    <button
                      className="btn book-now-btn mt-2"
                      onClick={() => handleBookNow(table.tableid)}
                    >
                      Book Now
                    </button>
                  )}
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </>
  );
}

export default UserHome;
