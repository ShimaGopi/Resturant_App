import React, { useState, useEffect } from 'react';
import './allTables.css';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import chairImage from '/public/chair.png';

function AllTables() {
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

  return (
    <>
      <div className="container mt-4">
        <div className="row">
        </div>
      </div>
      <h1 className='packages-heading mb-4'>All Tables</h1>
      <div className="container">
        <div className="row">
          {filteredTables.map((table) => (
            <div key={table.tableid} className="col-md-4 mb-4">
              <div className={`card card1 ${table.status == 2 ? 'booked-table' : 'available-table'}`}>
                <img src={chairImage} className="card-img-top" alt={table.tablename} />
                <div className="card-body">
                  <h3 className="card-title rec_Name main_ttl">{table.tablename}</h3>
                  <p className="card-text expl main_ttl">Status: {table.status == 1 ? 'Available' : 'Booked'}</p>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </>
  );
}

export default AllTables;
