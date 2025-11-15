/*
  # Enquiry Management System Database Schema

  1. New Tables
    - `customer_companies` - Stores customer, client, and consultant company details
      - `id` (uuid, primary key)
      - `category` (text) - Contractor/Client/Consultant
      - `company_name` (text, unique)
      - `address1` (text)
      - `address2` (text, nullable)
      - `rating` (text, nullable)
      - `type` (text, nullable)
      - `fax_no` (text, nullable)
      - `phone1` (text)
      - `phone2` (text, nullable)
      - `mail_id` (text, nullable)
      - `website` (text, nullable)
      - `status` (text) - Active/Inactive
      - `created_at` (timestamptz)
      - `updated_at` (timestamptz)

    - `contact_persons` - Stores contact person details
      - `id` (uuid, primary key)
      - `customer_company_id` (uuid, foreign key)
      - `contact_name` (text)
      - `designation` (text, nullable)
      - `category_of_designation` (text) - Technical/General
      - `address1` (text)
      - `address2` (text, nullable)
      - `fax_no` (text, nullable)
      - `phone` (text, nullable)
      - `mobile1` (text, nullable)
      - `mobile2` (text, nullable)
      - `email_id` (text, nullable)
      - `created_at` (timestamptz)
      - `updated_at` (timestamptz)

    - `users` - Stores system users (sales engineers, etc.)
      - `id` (uuid, primary key)
      - `full_name` (text)
      - `designation` (text, nullable)
      - `mail_id` (text, unique)
      - `login_password` (text) - Note: Should use proper auth in production
      - `status` (text) - Active/Inactive
      - `department` (text)
      - `roles` (text[]) - Array of roles
      - `created_at` (timestamptz)
      - `updated_at` (timestamptz)

    - `enquiry_for_items` - Stores enquiry categories/items
      - `id` (uuid, primary key)
      - `item_name` (text, unique)
      - `company_name` (text, nullable)
      - `department_name` (text, nullable)
      - `status` (text) - Active/Inactive
      - `common_mail_ids` (text[])
      - `cc_mail_ids` (text[])
      - `created_at` (timestamptz)
      - `updated_at` (timestamptz)

    - `enquiries` - Main enquiry table
      - `id` (uuid, primary key)
      - `request_no` (text, unique)
      - `source_of_info` (text)
      - `enquiry_date` (date)
      - `due_on` (date)
      - `site_visit_date` (date, nullable)
      - `project_name` (text)
      - `client_name` (text)
      - `consultant_name` (text, nullable)
      - `details_of_enquiry` (text)
      - `documents_received` (text, nullable)
      - `hardcopy` (boolean)
      - `drawing` (boolean)
      - `dvd` (boolean)
      - `spec` (boolean)
      - `eqpschedule` (boolean)
      - `remark` (text, nullable)
      - `auto_ack` (boolean)
      - `ceosign` (boolean)
      - `status` (text) - Enquiry/Quotation/Won/Lost/etc.
      - `selected_enquiry_types` (text[])
      - `created_at` (timestamptz)
      - `updated_at` (timestamptz)
      - `created_by` (uuid, nullable) - Future auth integration

    - `enquiry_customers` - Junction table for enquiry-customer relationship
      - `enquiry_id` (uuid, foreign key)
      - `customer_company_id` (uuid, foreign key)
      - Primary key: (enquiry_id, customer_company_id)

    - `enquiry_contacts` - Junction table for enquiry-contact relationship
      - `enquiry_id` (uuid, foreign key)
      - `contact_person_id` (uuid, foreign key)
      - Primary key: (enquiry_id, contact_person_id)

    - `enquiry_users` - Junction table for enquiry-user relationship
      - `enquiry_id` (uuid, foreign key)
      - `user_id` (uuid, foreign key)
      - Primary key: (enquiry_id, user_id)

    - `enquiry_items` - Junction table for enquiry-item relationship
      - `enquiry_id` (uuid, foreign key)
      - `enquiry_for_item_id` (uuid, foreign key)
      - Primary key: (enquiry_id, enquiry_for_item_id)

    - `enquiry_audit_log` - Audit trail for changes
      - `id` (uuid, primary key)
      - `enquiry_id` (uuid, foreign key)
      - `action` (text) - CREATE/UPDATE/DELETE/STATUS_CHANGE
      - `changed_by` (text) - User name/email
      - `changes` (jsonb) - Details of changes
      - `created_at` (timestamptz)

  2. Security
    - Enable RLS on all tables
    - Add policies for public access (temporary - will add auth later)

  3. Indexes
    - Create indexes for frequently queried columns
*/

-- Create customer_companies table
CREATE TABLE IF NOT EXISTS customer_companies (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  category text NOT NULL DEFAULT 'Contractor',
  company_name text NOT NULL UNIQUE,
  address1 text NOT NULL,
  address2 text,
  rating text,
  type text,
  fax_no text,
  phone1 text NOT NULL,
  phone2 text,
  mail_id text,
  website text,
  status text NOT NULL DEFAULT 'Active',
  created_at timestamptz DEFAULT now(),
  updated_at timestamptz DEFAULT now()
);

-- Create contact_persons table
CREATE TABLE IF NOT EXISTS contact_persons (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  customer_company_id uuid REFERENCES customer_companies(id) ON DELETE CASCADE,
  contact_name text NOT NULL,
  designation text,
  category_of_designation text NOT NULL DEFAULT 'Technical',
  address1 text NOT NULL,
  address2 text,
  fax_no text,
  phone text,
  mobile1 text,
  mobile2 text,
  email_id text,
  created_at timestamptz DEFAULT now(),
  updated_at timestamptz DEFAULT now()
);

-- Create users table
CREATE TABLE IF NOT EXISTS users (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  full_name text NOT NULL,
  designation text,
  mail_id text NOT NULL UNIQUE,
  login_password text NOT NULL DEFAULT 'password123',
  status text NOT NULL DEFAULT 'Active',
  department text NOT NULL DEFAULT 'MEP',
  roles text[] DEFAULT ARRAY[]::text[],
  created_at timestamptz DEFAULT now(),
  updated_at timestamptz DEFAULT now()
);

-- Create enquiry_for_items table
CREATE TABLE IF NOT EXISTS enquiry_for_items (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  item_name text NOT NULL UNIQUE,
  company_name text,
  department_name text,
  status text NOT NULL DEFAULT 'Active',
  common_mail_ids text[] DEFAULT ARRAY[]::text[],
  cc_mail_ids text[] DEFAULT ARRAY[]::text[],
  created_at timestamptz DEFAULT now(),
  updated_at timestamptz DEFAULT now()
);

-- Create enquiries table
CREATE TABLE IF NOT EXISTS enquiries (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  request_no text NOT NULL UNIQUE,
  source_of_info text NOT NULL,
  enquiry_date date,
  due_on date,
  site_visit_date date,
  project_name text NOT NULL,
  client_name text NOT NULL,
  consultant_name text,
  details_of_enquiry text NOT NULL,
  documents_received text,
  hardcopy boolean DEFAULT false,
  drawing boolean DEFAULT false,
  dvd boolean DEFAULT false,
  spec boolean DEFAULT false,
  eqpschedule boolean DEFAULT false,
  remark text,
  auto_ack boolean DEFAULT true,
  ceosign boolean DEFAULT false,
  status text NOT NULL DEFAULT 'Enquiry',
  selected_enquiry_types text[] DEFAULT ARRAY[]::text[],
  created_at timestamptz DEFAULT now(),
  updated_at timestamptz DEFAULT now(),
  created_by uuid
);

-- Create junction tables
CREATE TABLE IF NOT EXISTS enquiry_customers (
  enquiry_id uuid REFERENCES enquiries(id) ON DELETE CASCADE,
  customer_company_id uuid REFERENCES customer_companies(id) ON DELETE CASCADE,
  PRIMARY KEY (enquiry_id, customer_company_id)
);

CREATE TABLE IF NOT EXISTS enquiry_contacts (
  enquiry_id uuid REFERENCES enquiries(id) ON DELETE CASCADE,
  contact_person_id uuid REFERENCES contact_persons(id) ON DELETE CASCADE,
  PRIMARY KEY (enquiry_id, contact_person_id)
);

CREATE TABLE IF NOT EXISTS enquiry_users (
  enquiry_id uuid REFERENCES enquiries(id) ON DELETE CASCADE,
  user_id uuid REFERENCES users(id) ON DELETE CASCADE,
  PRIMARY KEY (enquiry_id, user_id)
);

CREATE TABLE IF NOT EXISTS enquiry_items (
  enquiry_id uuid REFERENCES enquiries(id) ON DELETE CASCADE,
  enquiry_for_item_id uuid REFERENCES enquiry_for_items(id) ON DELETE CASCADE,
  PRIMARY KEY (enquiry_id, enquiry_for_item_id)
);

-- Create audit log table
CREATE TABLE IF NOT EXISTS enquiry_audit_log (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  enquiry_id uuid REFERENCES enquiries(id) ON DELETE CASCADE,
  action text NOT NULL,
  changed_by text NOT NULL,
  changes jsonb,
  created_at timestamptz DEFAULT now()
);

-- Create indexes
CREATE INDEX IF NOT EXISTS idx_customer_companies_category ON customer_companies(category) WHERE status = 'Active';
CREATE INDEX IF NOT EXISTS idx_customer_companies_status ON customer_companies(status);
CREATE INDEX IF NOT EXISTS idx_contact_persons_company ON contact_persons(customer_company_id);
CREATE INDEX IF NOT EXISTS idx_enquiries_date ON enquiries(enquiry_date DESC);
CREATE INDEX IF NOT EXISTS idx_enquiries_status ON enquiries(status);
CREATE INDEX IF NOT EXISTS idx_enquiries_request_no ON enquiries(request_no);
CREATE INDEX IF NOT EXISTS idx_audit_log_enquiry ON enquiry_audit_log(enquiry_id, created_at DESC);

-- Enable Row Level Security
ALTER TABLE customer_companies ENABLE ROW LEVEL SECURITY;
ALTER TABLE contact_persons ENABLE ROW LEVEL SECURITY;
ALTER TABLE users ENABLE ROW LEVEL SECURITY;
ALTER TABLE enquiry_for_items ENABLE ROW LEVEL SECURITY;
ALTER TABLE enquiries ENABLE ROW LEVEL SECURITY;
ALTER TABLE enquiry_customers ENABLE ROW LEVEL SECURITY;
ALTER TABLE enquiry_contacts ENABLE ROW LEVEL SECURITY;
ALTER TABLE enquiry_users ENABLE ROW LEVEL SECURITY;
ALTER TABLE enquiry_items ENABLE ROW LEVEL SECURITY;
ALTER TABLE enquiry_audit_log ENABLE ROW LEVEL SECURITY;

-- Create RLS Policies (Public access for now - will add proper auth later)
CREATE POLICY "Public read access for customer_companies" ON customer_companies FOR SELECT USING (true);
CREATE POLICY "Public insert access for customer_companies" ON customer_companies FOR INSERT WITH CHECK (true);
CREATE POLICY "Public update access for customer_companies" ON customer_companies FOR UPDATE USING (true) WITH CHECK (true);
CREATE POLICY "Public delete access for customer_companies" ON customer_companies FOR DELETE USING (true);

CREATE POLICY "Public read access for contact_persons" ON contact_persons FOR SELECT USING (true);
CREATE POLICY "Public insert access for contact_persons" ON contact_persons FOR INSERT WITH CHECK (true);
CREATE POLICY "Public update access for contact_persons" ON contact_persons FOR UPDATE USING (true) WITH CHECK (true);
CREATE POLICY "Public delete access for contact_persons" ON contact_persons FOR DELETE USING (true);

CREATE POLICY "Public read access for users" ON users FOR SELECT USING (true);
CREATE POLICY "Public insert access for users" ON users FOR INSERT WITH CHECK (true);
CREATE POLICY "Public update access for users" ON users FOR UPDATE USING (true) WITH CHECK (true);
CREATE POLICY "Public delete access for users" ON users FOR DELETE USING (true);

CREATE POLICY "Public read access for enquiry_for_items" ON enquiry_for_items FOR SELECT USING (true);
CREATE POLICY "Public insert access for enquiry_for_items" ON enquiry_for_items FOR INSERT WITH CHECK (true);
CREATE POLICY "Public update access for enquiry_for_items" ON enquiry_for_items FOR UPDATE USING (true) WITH CHECK (true);
CREATE POLICY "Public delete access for enquiry_for_items" ON enquiry_for_items FOR DELETE USING (true);

CREATE POLICY "Public read access for enquiries" ON enquiries FOR SELECT USING (true);
CREATE POLICY "Public insert access for enquiries" ON enquiries FOR INSERT WITH CHECK (true);
CREATE POLICY "Public update access for enquiries" ON enquiries FOR UPDATE USING (true) WITH CHECK (true);
CREATE POLICY "Public delete access for enquiries" ON enquiries FOR DELETE USING (true);

CREATE POLICY "Public read access for enquiry_customers" ON enquiry_customers FOR SELECT USING (true);
CREATE POLICY "Public insert access for enquiry_customers" ON enquiry_customers FOR INSERT WITH CHECK (true);
CREATE POLICY "Public delete access for enquiry_customers" ON enquiry_customers FOR DELETE USING (true);

CREATE POLICY "Public read access for enquiry_contacts" ON enquiry_contacts FOR SELECT USING (true);
CREATE POLICY "Public insert access for enquiry_contacts" ON enquiry_contacts FOR INSERT WITH CHECK (true);
CREATE POLICY "Public delete access for enquiry_contacts" ON enquiry_contacts FOR DELETE USING (true);

CREATE POLICY "Public read access for enquiry_users" ON enquiry_users FOR SELECT USING (true);
CREATE POLICY "Public insert access for enquiry_users" ON enquiry_users FOR INSERT WITH CHECK (true);
CREATE POLICY "Public delete access for enquiry_users" ON enquiry_users FOR DELETE USING (true);

CREATE POLICY "Public read access for enquiry_items" ON enquiry_items FOR SELECT USING (true);
CREATE POLICY "Public insert access for enquiry_items" ON enquiry_items FOR INSERT WITH CHECK (true);
CREATE POLICY "Public delete access for enquiry_items" ON enquiry_items FOR DELETE USING (true);

CREATE POLICY "Public read access for enquiry_audit_log" ON enquiry_audit_log FOR SELECT USING (true);
CREATE POLICY "Public insert access for enquiry_audit_log" ON enquiry_audit_log FOR INSERT WITH CHECK (true);

-- Insert seed data
INSERT INTO customer_companies (company_name, category, status, address1, phone1) VALUES
  ('Customer X Ltd', 'Contractor', 'Active', '123 Main St', '222'),
  ('Customer Y Corp', 'Contractor', 'Active', '456 Oak Ave', '555'),
  ('Client Z Inc', 'Client', 'Active', '789 Pine Rd', '888'),
  ('Consultant A', 'Consultant', 'Active', '101 Elm Blvd', '000')
ON CONFLICT (company_name) DO NOTHING;

INSERT INTO contact_persons (contact_name, customer_company_id, email_id, designation, address1, mobile1, category_of_designation)
SELECT 'Velu', id, 'pa@custx.com', 'Manager', '123 Main St', '333', 'Technical'
FROM customer_companies WHERE company_name = 'Customer X Ltd'
ON CONFLICT DO NOTHING;

INSERT INTO contact_persons (contact_name, customer_company_id, email_id, designation, address1, mobile1, category_of_designation)
SELECT 'Vijay', id, 'pb@custy.com', 'Director', '456 Oak Ave', '666', 'Technical'
FROM customer_companies WHERE company_name = 'Customer Y Corp'
ON CONFLICT DO NOTHING;

INSERT INTO contact_persons (contact_name, customer_company_id, email_id, designation, address1, mobile1, category_of_designation)
SELECT 'Seema', id, 'sc@custx.com', 'Engineer', '123 Main St', '333', 'Technical'
FROM customer_companies WHERE company_name = 'Customer X Ltd'
ON CONFLICT DO NOTHING;

INSERT INTO users (full_name, designation, mail_id, status, roles, login_password, department) VALUES
  ('SE1 - John Doe', 'Sales Engineer', 'se1@comp.com', 'Active', ARRAY['Enquiry', 'Quotation'], '123', 'MEP'),
  ('SE2 - Jane Smith', 'Sales Manager', 'se2@comp.com', 'Active', ARRAY['Enquiry', 'Admin'], '123', 'MEP')
ON CONFLICT (mail_id) DO NOTHING;

INSERT INTO enquiry_for_items (item_name, department_name, common_mail_ids, cc_mail_ids, status) VALUES
  ('Electrical', 'Elect', ARRAY['elect_common@a.com'], ARRAY[]::text[], 'Active'),
  ('Mechanical', 'Mech', ARRAY[]::text[], ARRAY['mech_cc1@b.com'], 'Active')
ON CONFLICT (item_name) DO NOTHING;