import { useState, useEffect } from 'react';
import { getEmailAPI } from '../services/api';
import profileImage from '../assets/ProfileImage.png';
import './Profile.css';

const Profile = () => {
  const [activeTab, setActiveTab] = useState('profile');
  const [email, setEmail] = useState('');
  const [loading, setLoading] = useState(true);

  // Get userCode from localStorage
  const userCode = localStorage.getItem('userCode') || '123654';

  useEffect(() => {
    const fetchEmail = async () => {
      try {
        console.log('Profile: Fetching email for userCode:', userCode);
        const fetchedEmail = await getEmailAPI(userCode);
        console.log('Profile: Fetched email result:', fetchedEmail);
        
        if (fetchedEmail && fetchedEmail !== 'null' && fetchedEmail.trim() !== '') {
          setEmail(fetchedEmail);
          console.log('Profile: Email set successfully:', fetchedEmail);
        } else {
          console.warn('Profile: Email is empty, null, or invalid');
          setEmail('');
        }
      } catch (error) {
        console.error('Profile: Failed to fetch email:', error);
        console.error('Profile: Error details:', error.message);
        setEmail('');
      } finally {
        setLoading(false);
      }
    };

    if (userCode && userCode.trim() !== '') {
      fetchEmail();
    } else {
      console.warn('Profile: No userCode found in localStorage');
      setLoading(false);
    }
  }, [userCode]);

  // Sample user data - in real app, this would come from API/context
  const userData = {
    name: 'Neeraj Dhungana',
    userCode: userCode,
    email: email, // Email is fetched from GetEmail API
    fullName: 'Neeraj Dhungana',
    contactNumber: '+86342134234',
    country: 'Korea',
    address: 'Seoul, Korea',
    username: 'nrj.dhungana',
    passwordChangeWarningDays: 12
  };

  return (
    <div className="profile-page">
      <div className="profile-header">
        <div className="profile-icon-title">
          <svg className="page-icon" width="24" height="24" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
            <rect x="3" y="3" width="6" height="6" rx="1" fill="#9CA3AF"/>
            <rect x="11" y="3" width="6" height="6" rx="1" fill="#9CA3AF"/>
            <rect x="3" y="11" width="6" height="6" rx="1" fill="#9CA3AF"/>
            <rect x="11" y="11" width="6" height="6" rx="1" fill="#9CA3AF"/>
          </svg>
          <h1 className="page-title">Profile</h1>
        </div>
      </div>

      <div className="user-info-header">
        <div className="user-avatar-large">
          <img 
            src={profileImage} 
            alt="Profile" 
            className="user-avatar-image"
            onError={(e) => {
              console.error('Failed to load ProfileImage.png');
              e.target.style.display = 'none';
            }}
          />
        </div>
        <div className="user-info-text">
          <h2 className="user-name">{userData.name}</h2>
          <p className="user-code">User Code: {userData.userCode}</p>
        </div>
      </div>

      <div className="profile-tabs">
        <button
          className={`tab-button ${activeTab === 'profile' ? 'active' : ''}`}
          onClick={() => setActiveTab('profile')}
        >
          Profile
        </button>
        <button
          className={`tab-button ${activeTab === 'roles' ? 'active' : ''}`}
          onClick={() => setActiveTab('roles')}
        >
          Roles & Privillege
        </button>
      </div>

      {activeTab === 'profile' && (
        <div className="profile-content">
          <div className="basic-details-section">
            <h3 className="section-title">Basic Details</h3>
            <div className="details-grid">
              <div className="detail-item">
                <span className="detail-label">Full Name:</span>
                <span className="detail-value">{userData.fullName}</span>
              </div>
              <div className="detail-item">
                <span className="detail-label">Email address:</span>
                <span className="detail-value">
                  {loading ? 'Loading...' : (email && email.trim() !== '' ? email : 'N/A')}
                </span>
              </div>
              <div className="detail-item">
                <span className="detail-label">Contact Number:</span>
                <span className="detail-value">{userData.contactNumber}</span>
              </div>
              <div className="detail-item">
                <span className="detail-label">Country:</span>
                <span className="detail-value">{userData.country}</span>
              </div>
              <div className="detail-item">
                <span className="detail-label">Address:</span>
                <span className="detail-value">{userData.address}</span>
              </div>
              <div className="detail-item">
                <span className="detail-label">Username:</span>
                <span className="detail-value">{userData.username}</span>
              </div>
              <div className="detail-item">
                <span className="detail-label">Password Change Warning Days:</span>
                <span className="detail-value">{userData.passwordChangeWarningDays} days</span>
              </div>
            </div>
          </div>
        </div>
      )}

      {activeTab === 'roles' && (
        <div className="profile-content">
          <div className="basic-details-section">
            <h3 className="section-title">Roles & Privileges</h3>
            <p className="empty-state">Roles and privileges information will be displayed here.</p>
          </div>
        </div>
      )}
    </div>
  );
};

export default Profile;

