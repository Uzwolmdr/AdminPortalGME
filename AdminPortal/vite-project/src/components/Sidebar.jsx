import { useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import './Sidebar.css';

const Sidebar = ({ isOpen }) => {
  const navigate = useNavigate();
  const location = useLocation();

  const menuItems = [
    { id: 'dashboard', label: 'Dashboard', icon: 'dashboard', path: '/dashboard' },
    { id: 'customer', label: 'Customer Management', icon: 'person', hasDropdown: true },
    { id: 'transaction', label: 'Transaction', icon: 'document', hasDropdown: true },
  ];

  const [expandedItems, setExpandedItems] = useState([]);

  const toggleExpand = (itemId) => {
    setExpandedItems(prev =>
      prev.includes(itemId) ? prev.filter(id => id !== itemId) : [...prev, itemId]
    );
  };

  const isActive = (path) => {
    return location.pathname === path;
  };

  return (
    <aside className={`sidebar ${isOpen ? 'open' : 'closed'}`}>
      <div className="sidebar-search">
        <input
          type="text"
          placeholder="Search menu"
          className="sidebar-search-input"
        />
        <svg className="sidebar-search-icon" width="16" height="16" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M9 17C13.4183 17 17 13.4183 17 9C17 4.58172 13.4183 1 9 1C4.58172 1 1 4.58172 1 9C1 13.4183 4.58172 17 9 17Z" stroke="#9CA3AF" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
          <path d="M19 19L14.65 14.65" stroke="#9CA3AF" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
        </svg>
      </div>

      <nav className="sidebar-nav">
        {menuItems.map((item) => (
          <div key={item.id}>
            <div
              className={`nav-item ${isActive(item.path) ? 'active' : ''}`}
              onClick={() => {
                if (item.hasDropdown) {
                  toggleExpand(item.id);
                } else if (item.path) {
                  navigate(item.path);
                }
              }}
            >
              <div className="nav-item-content">
                {item.icon === 'dashboard' && (
                  <svg className="nav-icon" width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect x="3" y="3" width="6" height="6" rx="1" fill="currentColor"/>
                    <rect x="11" y="3" width="6" height="6" rx="1" fill="currentColor"/>
                    <rect x="3" y="11" width="6" height="6" rx="1" fill="currentColor"/>
                    <rect x="11" y="11" width="6" height="6" rx="1" fill="currentColor"/>
                  </svg>
                )}
                {item.icon === 'person' && (
                  <svg className="nav-icon" width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M10 10C12.7614 10 15 7.76142 15 5C15 2.23858 12.7614 0 10 0C7.23858 0 5 2.23858 5 5C5 7.76142 7.23858 10 10 10Z" fill="currentColor"/>
                    <path d="M10 12C5.58172 12 2 13.7909 2 16V20H18V16C18 13.7909 14.4183 12 10 12Z" fill="currentColor"/>
                  </svg>
                )}
                {item.icon === 'document' && (
                  <svg className="nav-icon" width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M4 4H16V16H4V4ZM2 4C2 2.89543 2.89543 2 4 2H16C17.1046 2 18 2.89543 18 4V16C18 17.1046 17.1046 18 16 18H4C2.89543 18 2 17.1046 2 16V4ZM6 6H14V8H6V6ZM6 10H14V12H6V10ZM6 14H10V16H6V14Z" fill="currentColor"/>
                  </svg>
                )}
                <span className="nav-label">{item.label}</span>
              </div>
              {item.hasDropdown && (
                <svg className="dropdown-arrow" width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                  <path d="M6 12L10 8L6 4" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
                </svg>
              )}
            </div>
          </div>
        ))}
      </nav>
    </aside>
  );
};

export default Sidebar;

