const API_BASE_URL = 'http://localhost:5099/api';

export const loginAPI = async (email, userCode, password) => {
  try {
    const response = await fetch(`${API_BASE_URL}/Login/LoginProcess`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        Email: email,
        UserCode: userCode,
        Password: password,
      }),
    });

    // Try to parse response even if status is not OK
    let data;
    try {
      const responseText = await response.text();
      if (responseText) {
        data = JSON.parse(responseText);
      }
    } catch (parseError) {
      console.error('Failed to parse response:', parseError);
    }

    // If response is not OK and we have error data, throw with the data
    if (!response.ok) {
      const error = new Error(data?.ResponseDescription || data?.message || `HTTP error! status: ${response.status}`);
      error.response = response;
      error.data = data;
      throw error;
    }

    // Return the data (which may contain ResponseCode and ResponseDescription)
    return data || {};
  } catch (error) {
    console.error('Login API error:', error);
    
    // If it's a network error, throw a user-friendly message
    if (error.message === 'Failed to fetch' || error.message.includes('NetworkError') || error.name === 'TypeError') {
      throw new Error('Unable to connect to the server. Please make sure the API is running on http://localhost:5099');
    }
    
    // If we have response data with ResponseDescription, preserve it
    if (error.data) {
      const enhancedError = new Error(error.data.ResponseDescription || error.data.responseDescription || error.message);
      enhancedError.data = error.data;
      throw enhancedError;
    }
    
    throw error;
  }
};

export const getVersionAPI = async () => {
  try {
    const response = await fetch(`${API_BASE_URL}/Login/GetVersion`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    // GetVersion returns a string directly (Ok(version) in C#)
    // Read as text first
    const textData = await response.text();
    if (textData) {
      // Remove quotes if JSON serialized and trim
      let cleanVersion = textData.trim();
      cleanVersion = cleanVersion.replace(/^["']|["']$/g, '');
      console.log('GetVersion API response:', cleanVersion);
      return cleanVersion;
    }
    return '1.0';
  } catch (error) {
    console.error('GetVersion API error:', error);
    throw error;
  }
};

export const getEmailAPI = async (userCode) => {
  try {
    console.log('Calling GetEmail API with userCode:', userCode);
    const requestBody = {
      UserCode: userCode,
    };
    console.log('Request body:', JSON.stringify(requestBody));
    
    const response = await fetch(`${API_BASE_URL}/Login/GetEmail`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(requestBody),
    });

    console.log('GetEmail API response status:', response.status, response.statusText);

    if (!response.ok) {
      const errorText = await response.text();
      console.error('GetEmail API error response:', errorText);
      throw new Error(`HTTP error! status: ${response.status}, message: ${errorText}`);
    }

    // GetEmail returns a string directly (Ok(email) in C#)
    // Read as text first (ASP.NET Core may serialize strings as JSON strings)
    const textData = await response.text();
    console.log('GetEmail API raw response:', textData);
    console.log('GetEmail API response length:', textData?.length);
    
    if (!textData || textData.trim() === '') {
      console.warn('GetEmail API returned empty response');
      return null;
    }
    
    // Try to parse as JSON first (ASP.NET Core might serialize strings as JSON)
    let email = null;
    try {
      const parsed = JSON.parse(textData);
      console.log('GetEmail API parsed JSON:', parsed);
      email = parsed;
    } catch (parseError) {
      // Not JSON, use as plain text
      console.log('GetEmail API: Not JSON, using as text');
      email = textData;
    }
    
    // Convert to string and clean
    let cleanEmail = String(email).trim();
    // Remove surrounding quotes if present
    cleanEmail = cleanEmail.replace(/^["']|["']$/g, '');
    console.log('GetEmail API cleaned response:', cleanEmail);
    
    // Handle null, empty, or invalid responses
    if (cleanEmail === 'null' || cleanEmail === '' || cleanEmail === 'undefined' || cleanEmail === null) {
      console.warn('GetEmail API returned null or empty after cleaning');
      return null;
    }
    
    return cleanEmail;
  } catch (error) {
    console.error('GetEmail API error:', error);
    throw error;
  }
};

export const changePasswordAPI = async (email, userCode, oldPassword, newPassword) => {
  try {
    const response = await fetch(`${API_BASE_URL}/Login/ChangePassword`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        Email: email,
        UserCode: userCode,
        OldPassword: oldPassword,
        NewPassword: newPassword,
      }),
    });

    // Try to parse response even if status is not OK
    let data;
    try {
      const responseText = await response.text();
      if (responseText) {
        data = JSON.parse(responseText);
      }
    } catch (parseError) {
      console.error('Failed to parse response:', parseError);
    }

    // If response is not OK and we have error data, throw with the data
    if (!response.ok) {
      const error = new Error(data?.ResponseDescription || data?.message || `HTTP error! status: ${response.status}`);
      error.response = response;
      error.data = data;
      throw error;
    }

    // Return the data (which may contain ResponseCode and ResponseDescription)
    return data || {};
  } catch (error) {
    console.error('ChangePassword API error:', error);
    
    // If it's a network error, throw a user-friendly message
    if (error.message === 'Failed to fetch' || error.message.includes('NetworkError') || error.name === 'TypeError') {
      throw new Error('Unable to connect to the server. Please make sure the API is running on http://localhost:5099');
    }
    
    // If we have response data with ResponseDescription, preserve it
    if (error.data) {
      const enhancedError = new Error(error.data.ResponseDescription || error.data.responseDescription || error.message);
      enhancedError.data = error.data;
      throw enhancedError;
    }
    
    throw error;
  }
};

