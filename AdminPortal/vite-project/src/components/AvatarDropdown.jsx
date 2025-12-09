import { useState, useRef, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import profileImage from '../assets/ProfileImage.png';
import './AvatarDropdown.css';

const AvatarDropdown = () => {
  const [isOpen, setIsOpen] = useState(false);
  const dropdownRef = useRef(null);
  const navigate = useNavigate();

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
        setIsOpen(false);
      }
    };

    document.addEventListener('mousedown', handleClickOutside);
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, []);

  const handleProfileClick = () => {
    navigate('/profile');
    setIsOpen(false);
  };

  const handleChangePasswordClick = () => {
    navigate('/change-password');
    setIsOpen(false);
  };

  return (
    <div className="avatar-dropdown-container" ref={dropdownRef}>
      <div 
        className="header-item profile-picture"
        onClick={() => setIsOpen(!isOpen)}
      >
        <div className="avatar">
          <img 
            src={profileImage} 
            alt="Profile" 
            className="avatar-image"
            onError={(e) => {
              console.error('Failed to load ProfileImage.png');
              e.target.style.display = 'none';
            }}
          />
        </div>
      </div>

      {isOpen && (
        <div className="dropdown-menu">
          <div className="dropdown-item" onClick={handleProfileClick}>
            <svg className="dropdown-icon" width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M8 8C10.2091 8 12 6.20914 12 4C12 1.79086 10.2091 0 8 0C5.79086 0 4 1.79086 4 4C4 6.20914 5.79086 8 8 8Z" fill="currentColor"/>
              <path d="M8 10C4.68629 10 2 12.6863 2 16H14C14 12.6863 11.3137 10 8 10Z" fill="currentColor"/>
            </svg>
            Profile
          </div>
          <div className="dropdown-item" onClick={handleChangePasswordClick}>
            <svg className="dropdown-icon" width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M12 8V4C12 1.79086 10.2091 0 8 0C5.79086 0 4 1.79086 4 4V8H2V16H14V8H12ZM6 4C6 2.89543 6.89543 2 8 2C9.10457 2 10 2.89543 10 4V8H6V4ZM12 10V14H4V10H12Z" fill="currentColor"/>
            </svg>
            Change Password
          </div>
        </div>
      )}
    </div>
  );
};

export default AvatarDropdown;

